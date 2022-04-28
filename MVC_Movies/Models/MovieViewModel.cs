using MVC_Movies.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class MovieViewModel : Movie
    {
        public Movie movie { get; set; }

        public List<Movie> movies { get; set; }

        public List<Actor> actors { get; set; }

        public MovieFilters movieFilters { get; set; }

        public List<int> actorsIDs { get; set; }
    }
}
