using Microsoft.EntityFrameworkCore;

namespace Hotel_Manager.DbContext
{
    public class DbManager : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-OGJ98U8\TEW_SQLEXPRESS;Initial Catalog=FRONTEND_RESERVATION;Integrated Security=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here
            modelBuilder.Entity<Reservation>().ToTable("reservation");
        }
    }

  
}
