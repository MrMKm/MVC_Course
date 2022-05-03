using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVC_Movies.Data;
using MVC_Movies.Models;
using MVC_Movies.Models.Dto;
using MVC_Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Repository.Implementations
{
    public class MovieArticleRepository : IMovieArticleRepository
    {
        private readonly RepositoryContext repositoryContext;
        private readonly IMapper _mapper;

        public MovieArticleRepository(RepositoryContext _repositoryContext, IMapper mapper)
        {
            repositoryContext = _repositoryContext;
            _mapper = mapper;
        }

        public async Task<MovieArticle> CreateMovieArticle(MovieArticle article)
        {
            await repositoryContext.MovieArticle.AddAsync(article);
            await repositoryContext.SaveChangesAsync();

            return article;
        }

        public async Task DeleteMovieArticle(MovieArticle article)
        {
            repositoryContext.MovieArticle.Remove(article);
            await repositoryContext.SaveChangesAsync();
        }

        public async Task<MovieArticle> GetArticleByID(int articleID)
        {
            var article = await repositoryContext.MovieArticle
                .FirstOrDefaultAsync(a => a.ID.Equals(articleID));

            return article;
        }

        public async Task<List<MovieArticle>> GetArticles()
        {
            return await repositoryContext.MovieArticle.ToListAsync();
        }

        public async Task<MovieArticle> UpdateMovieArticle(MovieArticleUpdateDto article, int ArticleID)
        {
            var DBarticle = repositoryContext.MovieArticle
                .FirstOrDefault(a => a.ID.Equals(ArticleID));

            _mapper.Map(article, DBarticle);

            await repositoryContext.SaveChangesAsync();

            return DBarticle;
        }
    }
}
