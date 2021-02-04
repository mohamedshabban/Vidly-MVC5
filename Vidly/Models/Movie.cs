using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {

        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateTime  ReleaseDate { get; set; }
        
        public DateTime  DateAdded { get; set; }
        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1,20)]
        public int NumberInStock { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
    }
}