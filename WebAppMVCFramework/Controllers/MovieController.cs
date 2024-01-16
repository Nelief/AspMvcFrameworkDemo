using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVCFramework.DTO;
using WebAppMVCFramework.Models;

using System.Data.Entity;
using WebAppMVCFramework.Controllers.ViewNames;

namespace WebAppMVCFramework.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Movie")]
        public ViewResult Index()
        {
            if (User.IsInRole(Ruoli.canManageMovies))
                return View(MovieControllerViews.Index);

            return View(MovieControllerViews.IndexReadOnly);
        }

        [Route("Movie/Details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(x => x.Genre).FirstOrDefault(x => x.Id == id);

            if (movie == null) { return HttpNotFound(); }

            return View(MovieControllerViews.Details, movie);
        }

        [Route("Movie/New")]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public ActionResult New()
        {
            MovieFormDTO dto = new MovieFormDTO()
            {
                Movie = new Movie(),
                Genres = _context.Genres.ToList(),
            };
            return View(MovieControllerViews.MovieForm, dto);
        }

        [Route("Movie/Edit/{id}")]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            MovieFormDTO dto = new MovieFormDTO()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View(MovieControllerViews.MovieForm, dto);
        }


        [HttpPost]
        [Route("Movie/Save")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var m = new MovieFormDTO()
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View(MovieControllerViews.MovieForm, m);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieOld = _context.Movies.Single(x => x.Id == movie.Id);
                movieOld.Title = movie.Title;
                movieOld.Stock = movie.Stock;
                movieOld.Genre = movie.Genre;
                movieOld.ReleaseDate = movie.ReleaseDate;
                movieOld.DateAdded = movie.DateAdded;
            }

            _context.SaveChanges();
            return RedirectToAction(MovieControllerViews.Index, "Movie");
        }

    }
}