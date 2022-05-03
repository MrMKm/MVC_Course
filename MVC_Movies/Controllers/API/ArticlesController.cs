using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Movies.Models;
using MVC_Movies.Models.Dto;
using MVC_Movies.Models.Filters;
using MVC_Movies.Repository.Implementations;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Controllers.API
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieArticleRepository _articleRepository;

        public ArticlesController(IActorRepository actorRepository, IMovieArticleRepository articleRepository)
        {
            _actorRepository = actorRepository;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        [Route("{MovieArticleID}")]
        public async Task<IActionResult> GetArticleByID(int MovieArticleID)
        {
            var article = await _articleRepository.GetArticleByID(MovieArticleID);

            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleRepository.GetArticles();

            if (!articles.Any())
                return NotFound();

            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(MovieArticleRequest movieArticle)
        {
            var article = await _articleRepository.CreateMovieArticle(movieArticle.ToEntity());

            return Ok(article);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(int articleID, MovieArticleUpdateDto movieArticle)
        {
            var article = await _articleRepository.GetArticleByID(articleID);

            if (article == null)
                return NotFound();

            article = await _articleRepository.UpdateMovieArticle(movieArticle, articleID);

            return Ok(article);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int MovieArticleID)
        {
            var article = await _articleRepository.GetArticleByID(MovieArticleID);

            if (article == null)
                return NotFound();

            await _articleRepository.DeleteMovieArticle(article);

            return Ok();
        }
    }
}
