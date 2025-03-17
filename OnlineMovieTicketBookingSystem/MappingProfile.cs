using AutoMapper;
using OnlineMovieTicketBookingSystem.Dtos;
using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketDto>();
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<UpdateTicketDto, Ticket>();
            CreateMap<CreatePaymentDto, PaymentDetail>();
            CreateMap<PaymentDetail, PaymentDetailDto>();
        }
    }
}