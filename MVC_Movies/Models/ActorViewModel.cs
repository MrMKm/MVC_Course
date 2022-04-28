﻿using MVC_Movies.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class ActorViewModel : Actor
    {
        public List<Actor> actors { get; set; }

        public ActorFilters actorFilters { get; set; } 

        public List<int> moviesIDs { get; set; }

        public List<Movie> movies { get; set; }
    }
}
