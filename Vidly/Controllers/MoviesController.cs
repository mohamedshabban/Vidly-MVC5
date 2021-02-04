using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Vidly.Controllers;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            if (User.IsInRole("CanManageMovies"))
                return View("List");
            return View("ReadOnlyList");
        }
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel(new Movie())
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }
        public ActionResult Details(int? id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

       [System.Web.Mvc.HttpPost]
       [System.Web.Mvc.Authorize(Roles= RoleName.CanManageMovie)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList(),
                };
                return View("New", viewModel);
            }
            movie.DateAdded=DateTime.Now;
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name; 
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }

        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Edit(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return RedirectToAction("Index");
            var viewModel = new MovieFormViewModel(movieInDb)
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovie)]
        public void Delete(int id)
        {
            var movieInDb = _context.Movies.Single(c => c.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}