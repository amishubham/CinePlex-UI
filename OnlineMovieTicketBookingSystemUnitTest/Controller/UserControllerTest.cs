using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMovieTicketBookingSystem.Controllers;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineUserTicketBookingSystem.Controllers;

namespace OnlineMovieTicketBookingSystemUnitTest.Controller
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IGenericRepository<User>> _mockUserRepo;
        private Mock<IAuthService> _mockAuthService;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUserRepo = new Mock<IGenericRepository<User>>();
            _mockAuthService = new Mock<IAuthService>();
            _controller = new UserController(_mockUserRepo.Object, _mockAuthService.Object);
        }

        [Test]
        public void AddUser_ReturnsOkResult_WhenUserIsAdded()
        {
            // Arrange
            var user = new User { UserName = "testuser", Password = "password" };
            _mockAuthService.Setup(service => service.UserNameExists(user.UserName)).Returns(false);
            _mockUserRepo.Setup(repo => repo.Add(user)).Returns(new Result<User> { IsSuccess = true, Data = user });

            // Act
            var result = _controller.AddUser(user);

            // Assert
            //Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            //Assert.IsInstanceOf<User>(okResult.Value);
            var returnValue = okResult.Value as User;
            Assert.That(user.UserName, Is.EqualTo(returnValue.UserName));
        }

        [Test]
        public void AddUser_ReturnsBadRequest_WhenUserNameExists()
        {
            // Arrange
            var user = new User { UserName = "testuser", Password = "password" };
            _mockAuthService.Setup(service => service.UserNameExists(user.UserName)).Returns(true);

            // Act
            var result = _controller.AddUser(user);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginDto = new LoginDto { UserName = "testuser", Password = "password" };
            var user = new User { UserName = "testuser", Password = BCrypt.Net.BCrypt.HashPassword("password") };
            _mockAuthService.Setup(service => service.UserNameExists(loginDto.UserName)).Returns(true);
            _mockAuthService.Setup(service => service.GetUserByUserName(loginDto.UserName)).Returns(user);
            _mockAuthService.Setup(service => service.GenerateJwtToken(loginDto)).Returns("token");

            // Act
            var result = _controller.Login(loginDto);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value.GetType().GetProperty("Token").GetValue(okResult.Value, null), Is.InstanceOf<string>());
        }

        [Test]
        public void Login_ReturnsBadRequest_WhenUserNameNotFound()
        {
            // Arrange
            var loginDto = new LoginDto { UserName = "testuser", Password = "password" };
            _mockAuthService.Setup(service => service.UserNameExists(loginDto.UserName)).Returns(false);

            // Act
            var result = _controller.Login(loginDto);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetUserById_ReturnsOkResult_WhenUserIsFound()
        {
            // Arrange
            var user = new User { Id = 1, UserName = "testuser" };
            _mockUserRepo.Setup(repo => repo.GetById(user.Id)).Returns(new Result<User> { IsSuccess = true, Data = user });

            // Act
            var result = _controller.GetUserById(user.Id);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            var returnValue = okResult.Value as User;
            Assert.That(user.Id, Is.EqualTo(returnValue.Id));
        }

        
    }
}