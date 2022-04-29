using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class ActorRate
    {
        public int ID { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ActorID { get; set; }

        public Actor Actor { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}
