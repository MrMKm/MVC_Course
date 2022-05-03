using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class MovieArticle
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishAt { get; set; }
        public string Source { get; set; }
        public string Category { get; set; }

        public MovieArticle(string Title, string Body, string Category, string Source = null)
        {
            this.Title = Title;
            this.Body = Body;
            this.Source = Source;
            this.Category = Category;
            this.PublishAt = DateTime.Now;
        }
    }
}
