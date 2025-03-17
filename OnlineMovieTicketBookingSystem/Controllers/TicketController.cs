
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;

namespace OnlineMovieTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IGenericRepository<Ticket> _repo;
        private readonly ITicketService _service;
        private readonly IMapper _mapper;

        public TicketController(IGenericRepository<Ticket> repo, ITicketService service, IMapper mapper)
        {
            _repo = repo;
            _service = service;
            _mapper = mapper;
        }

        //[Authorize(Roles = "User")]
        [HttpPost("AddTicket")]
        public ActionResult<Ticket> AddTicket([FromBody] CreateTicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);

            if (!_service.TicketAvailable(ticket))
            {
                return BadRequest("Ticket Not Available");
            }

            var result = _repo.Add(ticket);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

        [HttpGet("GetTicketsByUserid")]
        public ActionResult<List<Ticket>> GetTicketsByUserid(int userid)
        {
            var result = _repo.GetAll();
            result.Data = result.Data.Where(a => a.UserId == userid).ToList();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

       // [Authorize(Roles = "User")]
        [HttpGet("GetTicketById")]
        public ActionResult<Ticket> GetTicketById(int id)
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

        [HttpPut("UpdateTicket")]
        public ActionResult<Ticket> UpdateTicket([FromBody] UpdateTicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            var result = _repo.Update(ticket);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

        //[Authorize(Roles = "User")]
        [HttpDelete("DeleteTicket")]
        public ActionResult<Ticket> DeleteTicket(int id)
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