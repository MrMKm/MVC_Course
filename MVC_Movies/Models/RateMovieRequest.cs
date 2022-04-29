using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class RateMovieRequest
    {
        public int ID { get; set; }

        public int Stars { get; set; }

        public string Comments { get; set; }
    }
}
