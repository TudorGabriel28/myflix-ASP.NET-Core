using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DataAccess.Models.Accounts;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using DataAccess.Models.Parameters;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryAccount _repository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(IRepositoryAccount repository, IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _repository.GetByEmailWithDetailsAsync(model.Email);

            if (account == null || !account.IsVerified || !BC.Verify(model.Password, account.PasswordHash))
                throw new AppException("Email or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(account);
            var refreshToken = generateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from account
            removeOldRefreshTokens(account);

            // save changes to db
            await _repository.UpdateAsync(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var (refreshToken, account) = await getRefreshToken(token);

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            await _repository.UpdateAsync(account);
            
            // generate new jwt
            var jwtToken = generateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var (refreshToken, account) = await getRefreshToken(token);

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _repository.UpdateAsync(account);
        }

        public async Task Register(RegisterRequest model, string origin)
        {
            // validate
            if (await _repository.GetByEmailWithDetailsAsync(model.Email) != null)
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }

            // map model to new account object
            var account = _mapper.Map<Account>(model);

            // first registered account is an admin
            var isFirstAccount = await _repository.CountAccountsAsync() == 0;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.VerificationToken = randomTokenString();

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            await _repository.AddAsync(account);

            // send email
            sendVerificationEmail(account, origin);
        }

        public async Task VerifyEmail(string token)
        {
            var account = await _repository.GetByVerificationTokenAsync(token);

            if (account == null) throw new AppException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await _repository.UpdateAsync(account);
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _repository.GetByEmailWithDetailsAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            // create reset token that expires after 1 day
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            
            _repository.UpdateAsync(account);
            
            // send email
            sendPasswordResetEmail(account, origin);
        }

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            var account = await _repository.GetByValidResetTokenAsync(model.Token);

            if (account == null)
                throw new AppException("Invalid token");
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await _repository.GetByValidResetTokenAsync(model.Token);

            if (account == null)
                throw new AppException("Invalid token");

            // update password and remove reset token
            account.PasswordHash = BC.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _repository.UpdateAsync(account);
        }

        public async Task<PagedList<AccountResponse>> GetAll(AccountParameters accountParameters)
        {
            var accounts = await _repository.GetAllAsync(accountParameters);
    
            return _mapper.Map<PagedList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            // validate
            if (await _repository.GetByEmailWithDetailsAsync(model.Email) != null)
                throw new AppException($"Email '{model.Email}' is already registered");

            // map model to new account object
            var account = _mapper.Map<Account>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            await _repository.AddAsync(account);

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            // validate
            if (account.Email != model.Email && await _repository.GetByEmailWithDetailsAsync(model.Email) != null)
                throw new AppException($"Email '{model.Email}' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                account.PasswordHash = BC.HashPassword(model.Password);

            // copy model to account and save
            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            await _repository.UpdateAsync(account);

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _repository.DeleteAsync(id))
                throw new KeyNotFoundException("Account not found");
            return true;
        }


        // helper methods

        private async Task<Account> getAccount(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        private async Task<(RefreshToken, Account)> getRefreshToken(string token)
        {
            var account = await _repository.GetByRefreshTokenAsync(token);

            if (account == null) throw new AppException("Invalid token");
            
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            
            if (!refreshToken.IsActive) throw new AppException("Invalid token");
            
            return (refreshToken, account);
        }

        private string generateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private string randomTokenString()
        {
            using var CryptographicRandomNumber = RandomNumberGenerator.Create();
            var randomBytes = new byte[40];
            CryptographicRandomNumber.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void removeOldRefreshTokens(Account account)
        {
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        private void sendVerificationEmail(Account account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        private void sendPasswordResetEmail(Account account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }
    }
}
