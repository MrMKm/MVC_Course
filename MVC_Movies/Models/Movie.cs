using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Movies.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")] //Only letters with the start Capital letter
        public string Gender { get; set; }

        [Required]
        [StringLength(5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string Rating { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<UserRate> Rates { get; set; } 
    }
}
