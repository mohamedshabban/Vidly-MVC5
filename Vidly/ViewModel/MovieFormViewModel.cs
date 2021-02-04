using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        //public Movie Movie { get; set; }
        public int? Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        
        [Required]
        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        public MovieFormViewModel()
        {
            Id = 0;
        }


        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
        }
        public string Title
        {
            get
            {
                return (Id != 0) ? "Edit Movie" : "New Movie";
            }
        }
       
    }
}