

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;
using BCrypt.Net;

namespace OnlineUserTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _repo;
        private readonly IAuthService _service;

        public UserController(IGenericRepository<User> repo, IAuthService service)
        {
            _repo = repo;
            _service = service;
        }

        [HttpPost("AddUser")]
        public ActionResult<User> AddUser(User user)
        {
            if (_service.UserNameExists(user.UserName))
            {
                return BadRequest("Username already exists");
            }

            // Hash the password before storing it
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var result = _repo.Add(user);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginDto loginDto)
        {
            if (!_service.UserNameExists(loginDto.UserName))
                return BadRequest("Username Not Found");

            var user = _service.GetUserByUserName(loginDto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return BadRequest("Wrong Password");
            }

            var token = _service.GenerateJwtToken(loginDto);
            return Ok(new { Token = token });
        }

       // [Authorize(Roles = "User")]
        [HttpGet("GetUserById")]
        public ActionResult<User> GetUserById(int id)
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

        //[Authorize(Roles = "User")]
        [HttpPut("UpdateUser")]
        public ActionResult<User> UpdateUser(User user)
        {
            var result = _repo.Update(user);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

        [HttpDelete("DeleteUser")]
        public ActionResult<User> DeleteUser(int id)
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
        //[Authorize(Roles = "User,Admin")]
        [HttpGet("GetUserByUserName")]
        public ActionResult<User> GetUserByUserName(string username)
        {
            var user = _service.GetUserByUserName(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
    }
}

