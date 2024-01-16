using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAppMVCFramework.DTO.ApiDTO;
using WebAppMVCFramework.Models;

using System.Data.Entity;


namespace WebAppMVCFramework.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieApiDTO> Movies = _context.Movies.Include(x => x.Genre).ToList().Select(Mapper.Map<Movie, MovieApiDTO>);

            return Ok(Movies);
        }

        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);

            if (movie == null) return NotFound();

            return Ok(Mapper.Map<Movie, MovieApiDTO>(movie));
        }

        //POST /api/movies
        [HttpPost]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public IHttpActionResult CreateMovie(MovieApiDTO movieDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var movie = Mapper.Map<MovieApiDTO, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);

        }

        //PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieApiDTO movieDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var movieInDB = _context.Movies.Include(x => x.Genre).SingleOrDefault(x => x.Id == id);

            if (movieInDB == null) return NotFound();

            Mapper.Map<MovieApiDTO, Movie>(movieDto, movieInDB);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDB == null) return NotFound();

            _context.Movies.Remove(movieInDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}

