namespace OnlineMovieTicketBookingSystem.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool NotFound { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
