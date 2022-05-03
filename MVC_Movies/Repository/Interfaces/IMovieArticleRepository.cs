using MVC_Movies.Models;
using MVC_Movies.Models.Dto;
using MVC_Movies.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Repository.Interfaces
{
    public interface IMovieArticleRepository
    {
        public Task<MovieArticle> GetArticleByID(int articleID);
        public Task<List<MovieArticle>> GetArticles();
        public Task<MovieArticle> CreateMovieArticle(MovieArticle article);
        public Task<MovieArticle> UpdateMovieArticle(MovieArticleUpdateDto article, int ArticleID);
        public Task DeleteMovieArticle(MovieArticle article);
    }
}
