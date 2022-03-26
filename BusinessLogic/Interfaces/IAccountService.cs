using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Accounts;
using DataAccess.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
        Task Register(RegisterRequest model, string origin);
        Task VerifyEmail(string token);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task ValidateResetToken(ValidateResetTokenRequest model);
        Task ResetPassword(ResetPasswordRequest model);
        Task<PagedList<AccountResponse>> GetAll(AccountParameters accountParameters);
        Task<AccountResponse> GetById(int id);
        Task<AccountResponse> Create(CreateRequest model);
        Task<AccountResponse> Update(int id, UpdateRequest model);
        Task<bool> Delete(int id);
    }
}
