using Microsoft.EntityFrameworkCore;
using MVC_Movies.Data;
using MVC_Movies.Models;
using MVC_Movies.Models.Filters;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Repository.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly RepositoryContext repositoryContext;

        public MovieRepository(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            await repositoryContext.Movie.AddAsync(movie);

            await repositoryContext.SaveChangesAsync();

            return movie;
        }

        public async Task DeleteMovie(Movie movie)
        {
            var dbMovie = await repositoryContext.Movie.FirstOrDefaultAsync(m => m.ID.Equals(movie.ID));

            if (dbMovie == null)
                throw new NullReferenceException("Movie with ID not found");

            repositoryContext.Remove(movie);
            await repositoryContext.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByID(int MovieID)
        {
            var movie = await repositoryContext.Movie.FirstOrDefaultAsync(m => m.ID.Equals(MovieID));

            if (movie == null)
                return default;

            return movie;
        }

        public async Task<List<Movie>> GetMovies(MovieFilters filters)
        {
            var movies = repositoryContext.Movie.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Title))
                movies = movies.Where(m => m.Title.Contains(filters.Title));

            if(!string.IsNullOrEmpty(filters.Gender))
                movies = movies.Where(m => m.Gender.Contains(filters.Gender));

            if (!string.IsNullOrEmpty(filters.Rating))
                movies = movies.Where(m => m.Rating.Contains(filters.Rating));

            if (filters.From.HasValue && filters.To.HasValue)
                movies = movies.Where(m => m.ReleaseDate >= filters.From && m.ReleaseDate >= filters.To);

            if(filters.actor != null)
                movies = movies.Where(m => m.Actors.Contains(filters.actor));

            return await movies.ToListAsync();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var dbmovie = await repositoryContext.Movie.FirstOrDefaultAsync(m => m.ID.Equals(movie.ID));

            if (dbmovie == null)
                return default;

            dbmovie.Title = movie.Title;
            dbmovie.ReleaseDate = movie.ReleaseDate;
            dbmovie.Price = movie.Price;
            dbmovie.Gender = movie.Gender;
            dbmovie.Rating = movie.Rating;

            await repositoryContext.SaveChangesAsync();

            return dbmovie;
        }
    }
}
