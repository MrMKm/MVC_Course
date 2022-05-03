using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class MovieArticleRequest
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(500)]
        public string Body { get; set; }

        [Url]
        public string Source { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; }

        public MovieArticle ToEntity()
        {
            return new MovieArticle(Title, Body, Category, Source);
        }
    }
}
