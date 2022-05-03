using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Movies.Models.Dto;
using MVC_Movies.Models.Filters;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers.API
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsControllers : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieArticleRepository _articleRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper; 

        public StatisticsControllers(IActorRepository actorRepository, IMovieArticleRepository articleRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _articleRepository = articleRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("articles")]
        public async Task<IActionResult> GetLastArticles()
        {
            var DBarticles = await _articleRepository.GetArticles();

            if (DBarticles == null)
                return NotFound();

            var articles = new List<ArticleStatisticsDto>();

            foreach (var article in DBarticles.TakeLast(5))
            {
                articles.Add(new ArticleStatisticsDto
                {
                    Title = article.Title,
                    PublishAt = article.PublishAt,
                    Body = article.Body.Length > 50 ? article.Body.Substring(0, 50) : article.Body
                });
            }

            return Ok(articles);
        }

        [HttpGet]
        [Route("movie-release")]
        public async Task<IActionResult> GetLastMovies()
        {
            var DBMovies = await _movieRepository.GetMoviesWithRates();

            if (DBMovies == null)
                return NotFound();

            var movies = new List<MovieStatisticDto>();

            foreach(var movie in DBMovies.Take(5))
            {
                movies.Add(new MovieStatisticDto
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate.Date,
                    Rate = movie.Rates.Count > 1 ? movie.Rates.Select(r => r.Stars).Average() : 0
                });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("movie-rating")]
        public async Task<IActionResult> GetBestMovies()
        {
            var DBMovies = await _movieRepository.GetMoviesWithRates();

            if (DBMovies == null)
                return NotFound();

            var movies = new List<MovieStatisticDto>();

            foreach (var movie in DBMovies.Take(5))
            {
                movies.Add(new MovieStatisticDto
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate.Date,
                    Rate = movie.Rates.Any() ? movie.Rates.Select(r => r.Stars).Average() : 0
                });
            }

            movies = movies.OrderByDescending(m => m.Rate).ToList();

            return Ok(movies);
        }
    }
}
