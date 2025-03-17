using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly MovieContext _context;

        public UserRepository(MovieContext context)
        {
            _context = context;
        }
        public Result<User> GetByUserName(string username)
        {
            try
            {
                var entity = _context.Users.FirstOrDefault(x=> x.UserName == username);
                if (entity == null)
                {
                    return new Result<User>
                    {
                        IsSuccess = true,
                        NotFound = true,
                        Data = null
                    };
                }
                return new Result<User>
                {
                    IsSuccess = true,

                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new Result<User>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }
    }
}
