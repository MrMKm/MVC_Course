using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [Range(1, 10)]
        public int OscarWon { get; set; }

        public Gender gender { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
