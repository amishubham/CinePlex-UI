

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicketBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRepository<PaymentDetail> _paymentRepo;
        private readonly IGenericRepository<Ticket> _ticketRepo;
        private readonly PaymentService _paymentService;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IGenericRepository<PaymentDetail> paymentRepo, IGenericRepository<Ticket> ticketRepo, PaymentService paymentService, EmailService emailService, IMapper mapper, ILogger<PaymentController> logger)
        {
            _paymentRepo = paymentRepo;
            _ticketRepo = ticketRepo;
            _paymentService = paymentService;
            _emailService = emailService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpPost("MakePayment")]
        public async Task<ActionResult<PaymentDetail>> MakePayment([FromBody] CreatePaymentDto paymentDto)
        {
            _logger.LogInformation("MakePayment called with TicketId: {TicketId}, Amount: {Amount}", paymentDto.TicketId, paymentDto.Amount);

            var ticketResult = _ticketRepo.GetById(paymentDto.TicketId);
            if (!ticketResult.IsSuccess || ticketResult.NotFound)
            {
                _logger.LogWarning("Ticket not found for TicketId: {TicketId}", paymentDto.TicketId);
                return NotFound("Ticket not found");
            }

            var orderId = _paymentService.CreateOrder(paymentDto.Amount);
            var paymentSuccess = _paymentService.ProcessPayment(orderId, paymentDto.Amount, paymentDto.PaymentMethod);

            if (!paymentSuccess)
            {
                _logger.LogError("Payment processing failed for OrderId: {OrderId}", orderId);
                return BadRequest("Payment processing failed");
            }

            var payment = _mapper.Map<PaymentDetail>(paymentDto);
            payment.PaymentDate = DateTime.Now;

            var paymentResult = _paymentRepo.Add(payment);
            if (paymentResult.IsSuccess)
            {
                if (ticketResult.Data?.User?.Email == null)
                {
                    _logger.LogWarning("User information is missing for the ticket with TicketId: {TicketId}", paymentDto.TicketId);
                    return BadRequest("User information is missing for the ticket");
                }
                var userEmail = ticketResult.Data.User.Email;
                // Send confirmation email
                var subject = "Payment Confirmation";
                var body = $"Your payment of {paymentDto.Amount} for ticket ID {paymentDto.TicketId} has been successfully processed.";
                await _emailService.SendEmailAsync(userEmail, subject, body);

                _logger.LogInformation("Payment successful for TicketId: {TicketId}, OrderId: {OrderId}", paymentDto.TicketId, orderId);
                return Ok(paymentResult.Data);
            }

            _logger.LogError("Failed to add payment for TicketId: {TicketId}", paymentDto.TicketId);
            return BadRequest(paymentResult.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllPayments")]
        public ActionResult<List<PaymentDetail>> GetAllPayments()
        {
            _logger.LogInformation("GetAllPayments called");

            var result = _paymentRepo.GetAll();
            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully retrieved all payments");
                return Ok(result.Data);
            }

            _logger.LogError("Failed to retrieve all payments");
            return BadRequest(result.Data);
        }
    }
}
    

