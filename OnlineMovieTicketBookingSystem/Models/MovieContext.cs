using Microsoft.EntityFrameworkCore;

namespace OnlineMovieTicketBookingSystem.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public object Movies { get; internal set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            //below new
            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,2)");
            });
        }

    }
    }

