using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models.Dto
{
    public class ArticleStatisticsDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishAt { get; set; }
    }
}
