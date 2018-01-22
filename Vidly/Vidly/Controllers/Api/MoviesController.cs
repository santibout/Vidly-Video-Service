using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        //protected override void Dispose(bool disposing)
        //{
        //    _context.Dispose();
        //}
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }
        public IHttpActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var f = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (f == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map(movieDto, f);

            _context.SaveChanges();
        }
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(f => f.Id == id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
