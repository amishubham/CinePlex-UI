using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;

namespace OnlineMovieTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
  //  [Authorize(Roles= "Admin")]
    [ApiController]
    public class TheatreController : ControllerBase
    {
        private readonly IGenericRepository<Theatre> _repo;
        public TheatreController(IGenericRepository<Theatre> Repo)
        {
            _repo = Repo;
        }
        [HttpPost("AddTheatre")]
        public ActionResult<Theatre> AddTheatre(Theatre theatre)
        {
            var result = _repo.Add(theatre);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
        [HttpGet("GetAllTheatres")]
        public ActionResult<List<Theatre>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
        [HttpGet("GetTheatreById")]
        public ActionResult<Theatre> GetTheatreById(int id)
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
        [HttpPut("UpdateTheatre")]
        public ActionResult<Theatre> UpdateTheatre(Theatre theatre)
        {
            var result = _repo.Update(theatre);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
        [HttpDelete("DeleteTheatre")]
        public ActionResult<Theatre> DeleteTheatre(int id)
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
    }
}
