using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;


namespace OnlineShowTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IGenericRepository<Show> _repo;
        private readonly IShowService _service;

        public ShowController(IGenericRepository<Show> Repo, IShowService service)
        {
            _repo = Repo;
            _service = service;
        }
      // [Authorize(Roles = "Admin")]
        [HttpPost("AddShow")]
        public ActionResult<Show> AddShow(Show show)
        {
            if(!_service.ValidShow(show))
            {
                return BadRequest("Invalid Show");
            }
            var result = _repo.Add(show);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
        [HttpGet("GetAllShows")]
        //[Authorize(Roles = "Admin,User")]
        public ActionResult<List<Show>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
        [HttpGet("GetShowById")]
       // [Authorize(Roles = "Admin,User")]
        public ActionResult<Show> GetShowById(int id)
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
        [HttpPut("UpdateShow")]
        public ActionResult<Show> UpdateShow(Show show)
        {

            if (!_service.ValidShow(show))
            {
                return BadRequest("Invalid Show");
            }
            var result = _repo.Update(show);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
      //  [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteShow")]
        public ActionResult<Show> DeleteShow(int id)
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
