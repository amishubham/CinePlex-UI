using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;
using OnlineMovieTicketBookingSystem.Repositories;
using OnlineMovieTicketBookingSystem.Services;

namespace OnlineMovieTicketBookingSystem.Dtos
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int UserId { get; set; }
        public int NoOfTickets { get; set; }
        public string SeatNos { get; set; }
        public DateTime ShowTiming { get; set; }
    }
}