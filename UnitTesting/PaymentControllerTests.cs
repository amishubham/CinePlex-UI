using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace TestProject
{
    [TestClass]
    public class PaymentControllerTests
    {
        private Mock<IGenericRepository<PaymentDetail>> _mockPaymentRepo;
        private Mock<IGenericRepository<Ticket>> _mockTicketRepo;
        private Mock<PaymentService> _mockPaymentService;
        private Mock<EmailService> _mockEmailService;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<PaymentController>> _mockLogger;
        private PaymentController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockPaymentRepo = new Mock<IGenericRepository<PaymentDetail>>();
            _mockTicketRepo = new Mock<IGenericRepository<Ticket>>();
            _mockPaymentService = new Mock<PaymentService>();
            _mockEmailService = new Mock<EmailService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<PaymentController>>();
            _controller = new PaymentController(
                _mockPaymentRepo.Object,
                _mockTicketRepo.Object,
                _mockPaymentService.Object,
                _mockEmailService.Object,
                _mockMapper.Object,
                _mockLogger.Object);
        }

        [TestMethod]
        public async Task MakePayment_ReturnsOkResult_WhenPaymentIsSuccessful()
        {
            // Arrange
            var paymentDto = new CreatePaymentDto { TicketId = 1, Amount = 100, PaymentMethod = "CreditCard" };
            var ticket = new Ticket { Id = 1, User = new User { Email = "user@example.com" } };
            var paymentDetail = new PaymentDetail { Id = 1, TicketId = 1, Amount = 100 };

            _mockTicketRepo.Setup(repo => repo.GetById(paymentDto.TicketId))
                .Returns(new Result<Ticket> { IsSuccess = true, Data = ticket });
            _mockPaymentService.Setup(service => service.CreateOrder(paymentDto.Amount)).Returns("orderId");
            _mockPaymentService.Setup(service => service.ProcessPayment("orderId", paymentDto.Amount, paymentDto.PaymentMethod)).Returns(true);
            _mockMapper.Setup(mapper => mapper.Map<PaymentDetail>(paymentDto)).Returns(paymentDetail);
            _mockPaymentRepo.Setup(repo => repo.Add(paymentDetail)).Returns(new Result<PaymentDetail> { IsSuccess = true, Data = paymentDetail });

            // Act
            var result = await _controller.MakePayment(paymentDto);

            // Assert
            var okResult = Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var returnValue = Assert.IsInstanceOfType(((OkObjectResult)okResult).Value, typeof(PaymentDetail));
            Assert.AreEqual(paymentDetail.Id, ((PaymentDetail)returnValue).Id);
        }

        [TestMethod]
        public async Task MakePayment_ReturnsNotFound_WhenTicketIsNotFound()
        {
            // Arrange
            var paymentDto = new CreatePaymentDto { TicketId = 1, Amount = 100, PaymentMethod = "CreditCard" };

            _mockTicketRepo.Setup(repo => repo.GetById(paymentDto.TicketId))
                .Returns(new Result<Ticket> { IsSuccess = false, NotFound = true });

            // Act
            var result = await _controller.MakePayment(paymentDto);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void GetAllPayments_ReturnsOkResult_WithListOfPayments()
        {
            // Arrange
            var payments = new List<PaymentDetail>
            {
                new PaymentDetail { Id = 1, TicketId = 1, Amount = 100 },
                new PaymentDetail { Id = 2, TicketId = 2, Amount = 200 }
            };

            _mockPaymentRepo.Setup(repo => repo.GetAll())
                .Returns(new Result<List<PaymentDetail>> { IsSuccess = true, Data = payments });

            // Act
            var result = _controller.GetAllPayments();

            // Assert
            var okResult = Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var returnValue = Assert.IsInstanceOfType(((OkObjectResult)okResult).Value, typeof(List<PaymentDetail>));
            Assert.AreEqual(2, ((List<PaymentDetail>)returnValue).Count);
        }
    }
}
