﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOS
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        public DateTime  ReleaseDate { get; set; }
        
        public DateTime  DateAdded { get; set; }
        [Required]
        [Range(1,20)]
        public int NumberInStock { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }
    }
}