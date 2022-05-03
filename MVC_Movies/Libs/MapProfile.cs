using MVC_Movies.Models;
using MVC_Movies.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Libs
{
    public class MapProfile : AutoMapper.Profile
    {
        public MapProfile()
        {
            CreateMap<Movie, MovieStatisticDto>();
            CreateMap<MovieArticle, ArticleStatisticsDto>();
            CreateMap<MovieArticle, MovieArticleUpdateDto>();
            CreateMap<MovieArticleUpdateDto, MovieArticle>();
        }
    }
}
