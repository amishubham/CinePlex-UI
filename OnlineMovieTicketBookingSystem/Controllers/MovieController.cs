
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IGenericRepository<Movie> _repo;
        private readonly IGenericRepository<Show> _showRepo;

        public MovieController(IGenericRepository<Movie> repo, IGenericRepository<Show> showRepo)
        {
            _repo = repo;
            _showRepo = showRepo;
        }

      //  [Authorize(Roles = "Admin")]
        [HttpPost("AddMovie")]
        public ActionResult<Movie> AddMovie(Movie movie)
        {
            var result = _repo.Add(movie);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

     //  [Authorize(Roles = "User")]
        [HttpGet("GetAllMovies")]
        public ActionResult<List<Movie>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

        [HttpGet("GetMovieById")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            var result = _repo.GetById(id);
            if (result.IsSuccess)
            {
                if (result.NotFound)
                {
                    return NotFound(result.Data);
                }
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

      // [Authorize(Roles = "Admin")]
        [HttpPut("UpdateMovie")]
        public ActionResult<Movie> UpdateMovie(Movie movie)
        {
            var result = _repo.Update(movie);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

     //  [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMovie")]
        public ActionResult<Movie> DeleteMovie(int id)
        {
            var result = _repo.Delete(id);
            if (result.IsSuccess)
            {
                if (result.NotFound)
                {
                    return NotFound(result.Data);
                }
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

      //  [Authorize(Roles = "User")]
        [HttpGet("GetMovieByMovieName")]
        public ActionResult<Movie> GetMovieByMovieName(string movieName)
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
            {
                var movie = result.Data.FirstOrDefault(m => m.Name == movieName);
                if (movie != null)
                {
                    return Ok(movie);
                }
                return NotFound("Movie not found");
            }
            return BadRequest(result.Data);
        }

     //   [Authorize(Roles = "User")]
        [HttpGet("GetMovieByTheatre")]
        public ActionResult<List<Movie>> GetMovieByTheatre(string theatreName)
        {
            var showResult = _showRepo.GetAll();
            if (showResult.IsSuccess)
            {
                var movieIds = showResult.Data.Where(s => s.TheatreName == theatreName).Select(s => s.MovieId).Distinct().ToList();
                var movieResult = _repo.GetAll();
                if (movieResult.IsSuccess)
                {
                    var movies = movieResult.Data.Where(m => movieIds.Contains(m.Id)).ToList();
                    if (movies.Any())
                    {
                        return Ok(movies);
                    }
                    return NotFound("No movies found for the specified theatre");
                }
                return BadRequest(movieResult.Data);
            }
            return BadRequest(showResult.Data);
        }
    }
}