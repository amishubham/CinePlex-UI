
using OnlineMovieTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using OnlineMovieTicketBookingSystem.Models;

namespace OnlineMovieTicketBookingSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieContext _context;

        public GenericRepository(MovieContext context)
        {
            _context = context;
        }

        public Result<T> Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return new Result<T>
                {
                    IsSuccess = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public Result<List<T>> GetAll()
        {
            try
            {
                var entities = _context.Set<T>().ToList();
                return new Result<List<T>>
                {
                    IsSuccess = true,
                    Data = entities
                };
            }
            catch (Exception ex)
            {
                return new Result<List<T>>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public Result<T> GetById(int id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                if (entity == null)
                {
                    return new Result<T>
                    {
                        IsSuccess = false,
                        NotFound = true,
                        Data = null,
                        Message = "Entity not found"
                    };
                }
                return new Result<T>
                {
                    IsSuccess = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public Result<T> Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
                return new Result<T>
                {
                    IsSuccess = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public Result<T> Delete(int id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                if (entity == null)
                {
                    return new Result<T>
                    {
                        IsSuccess = false,
                        NotFound = true,
                        Data = null,
                        Message = "Entity not found"
                    };
                }
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
                return new Result<T>
                {
                    IsSuccess = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public Result<List<Movie>> GetMovieByTheatre(string theatreName)
        {
            try
            {
                var shows = _context.Set<Show>().Where(s => s.TheatreName == theatreName).ToList();
                var movieIds = shows.Select(s => s.MovieId).Distinct().ToList();
                var movies = _context.Set<Movie>().Where(m => movieIds.Contains(m.Id)).ToList();

                return new Result<List<Movie>>
                {
                    IsSuccess = true,
                    Data = movies
                };
            }
            catch (Exception ex)
            {
                return new Result<List<Movie>>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public Result<T> GetMovieByTheatre(T entity)
        {
            throw new NotImplementedException();
        }

        Result<T> IGenericRepository<T>.GetMovieByTheatre(string theatreName)
        {
            throw new NotImplementedException();
        }
    }
}