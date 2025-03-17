using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem.Repositories
{
    public interface IGenericRepository<T>
    {
        Result<T> Add(T entity);
        Result<List<T>> GetAll();
        Result<T> GetById(int id);
        Result<T> Update(T entity);
        Result<T> Delete(int id);
        Result<T> GetMovieByTheatre(string theatreName);

    }
}
