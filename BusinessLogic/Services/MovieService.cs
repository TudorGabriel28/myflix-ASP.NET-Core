using BusinessLogic.Interfaces;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;
        private readonly IRepositoryMovie _repositoryMovie;
        private readonly IRepositoryGenre _repositoryGenre;

        public MovieService(
            IHttpClientFactory httpClientFactory, 
            IOptions<AppSettings> appSettings, 
            IRepositoryMovie repositoryMovie,
            IRepositoryGenre repositoryGenre
            )
        {
            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings.Value;
            _repositoryMovie = repositoryMovie;
            _repositoryGenre = repositoryGenre;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var movies = await _repositoryMovie.GetAllAsync();
            return movies;
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _repositoryMovie.GetByIdAsync(id);
            return movie;
        }

        public async Task<Movie> Create(string imdbMovieId, int accountId)
        {
            // verify if movie already exists in db
            var movie = await _repositoryMovie.GetByImdbIdAsync(imdbMovieId);
            if (movie != null)
            {
                return movie;
            }
            
            // make api call to RapidApi Data-Imdb API for movie information
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://data-imdb1.p.rapidapi.com/titles/{imdbMovieId}?info=base_info");
            request.Headers.Add("X-RapidAPI-Host", _appSettings.XRapidAPIHost);
            request.Headers.Add("X-RapidAPI-Key", _appSettings.XRapidAPIKey);
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // get the imdb movie json data from data-imdb api
                var imdbAPIResponse = await response.Content.ReadFromJsonAsync<ImdbAPIResponse>();
                // select the necessary properties from the response and create the movie object
                movie = await ImdbAPIResponseToMovie(imdbAPIResponse, accountId);
                // add movie to db
                await _repositoryMovie.AddAsync(movie);
                return movie;
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _repositoryMovie.DeleteAsync(id))
                throw new KeyNotFoundException("Movie not found");
            return true;
        }

        // helper methods

        private async Task<Movie> ImdbAPIResponseToMovie(ImdbAPIResponse model, int accountId)
        {
            // make an integer date format from json data
            int date = model.results.releaseDate.year * 10000 + model.results.releaseDate.month * 100 + model.results.releaseDate.day;

            Movie movie = new Movie
            {
                ImdbId = model.results.id,
                Title = model.results.titleText.text,
                TitleType = model.results.titleType.text,
                IsSeries = model.results.titleType.isSeries,
                IsEpisode = model.results.titleType.isEpisode,
                AggregateRating = model.results.ratingsSummary.aggregateRating,
                VoteCount = model.results.ratingsSummary.voteCount,
                ReleaseDate = DateTime.ParseExact(date.ToString(), "yyyyMMdd", null),
                RuntimeSeconds = model.results.runtime.seconds,
                Plot = model.results.plot.plotText.plainText,
                UploadedById = accountId
            };

            // if the movie is a series create new episodes object and add it to movie
            if(movie.IsSeries)
            {
                Episodes episodes = new Episodes
                {
                    SeasonsNumber = model.results.episodes.seasons.Length,
                    FirstYear = model.results.episodes.years.First().year,
                    LastYear = model.results.episodes.years.Last().year,
                    TotalEpisodes = model.results.episodes.totalEpisodes.total
                };

                movie.Episodes = episodes;
            }

            // create new primary image object
            PrimaryImage image = new PrimaryImage
            {
                Width = model.results.primaryImage.width,
                Height = model.results.primaryImage.height,
                Url = model.results.primaryImage.url,
                Caption = model.results.primaryImage.caption.plainText
            };
            // append image obj to movie
            movie.PrimaryImage = image;

            // create new meterranking object
            MeterRanking meterRanking = new MeterRanking 
            {
                CurrentRank = model.results.meterRanking.currentRank,
                ChangeDirection = model.results.meterRanking.rankChange.changeDirection,
                Difference = model.results.meterRanking.rankChange.difference
            };
            // append meterranking obj to movie
            movie.MeterRanking = meterRanking;

            // iterate through obtained genres, verify if genre already exists in db and append it to movie
            movie.Genres = new List<Genre>();
            foreach (ImdbGenre imdbGenre in model.results.genres.genres)
            {
                var genre = await _repositoryGenre.GetByNameAsync(imdbGenre.id);
                // if genre doesn't exist in db
                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = imdbGenre.text
                    };
                }
                movie.Genres.Add(genre);
            }

            return movie;
        }
    }
}
