using MVC_Movies.Models;
using MVC_Movies.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Repository.Interfaces
{
    public interface IMovieRepository
    {
        public Task<Movie> GetMovieByID(int MovieID);
        public Task<List<Movie>> GetMovies(MovieFilters filters);
        public Task<Movie> CreateMovie(Movie movie);
        public Task<Movie> UpdateMovie(Movie movie);
        public Task DeleteMovie(Movie movie);
        public Task<bool> RateMovie(UserRate rate);

        public Task<Movie> GetMovieWithRates(int MovieID);
    }
}
