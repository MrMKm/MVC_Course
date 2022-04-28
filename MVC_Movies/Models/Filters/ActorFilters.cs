using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models.Filters
{
    public class ActorFilters
    {
        public string Name { get; set; } = "";

        [DataType(DataType.Date)]
        public Nullable<DateTime> From { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> To { get; set; }
    }
}
