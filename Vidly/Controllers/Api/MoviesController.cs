using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.DTOS;
using Vidly.Models;
using System.Data.Entity;
namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public MoviesController()
        {
            _context=new ApplicationDbContext();
        }
        //Get
        //api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies
                .Include(m=>m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie,MovieDto>);
        }
        //api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);
            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }
        //api/movies/
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movie=Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri+"/"+movie.Id),movieDto);
        } 

        //api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if(movie==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
