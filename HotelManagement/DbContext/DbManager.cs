using Hotel_Manager.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;


public class DbManager : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-OGJ98U8\\TEW_SQLEXPRESS;Initial Catalog=FRONTEND_RESERVATION;Integrated Security=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
        v => v.ToDateTime(TimeOnly.MinValue),
        v => DateOnly.FromDateTime(v));

       
        modelBuilder.Entity<Reservation>().ToTable("reservation");

        modelBuilder.Entity<Reservation>().Property(r => r.Id).HasColumnName("id");
        modelBuilder.Entity<Reservation>().Property(r => r.FirstName).HasColumnName("first_name");
        modelBuilder.Entity<Reservation>().Property(r => r.LastName).HasColumnName("last_name");
        modelBuilder.Entity<Reservation>().Property(r => r.BirthDay).HasColumnName("birth_day");
        modelBuilder.Entity<Reservation>().Property(r => r.Gender).HasColumnName("gender");
        modelBuilder.Entity<Reservation>().Property(r => r.PhoneNumber).HasColumnName("phone_number");
        modelBuilder.Entity<Reservation>().Property(r => r.EmailAddress).HasColumnName("email_address");
        modelBuilder.Entity<Reservation>().Property(r => r.NumberGuest).HasColumnName("number_guest");
        modelBuilder.Entity<Reservation>().Property(r => r.StreetAddress).HasColumnName("street_address");
        modelBuilder.Entity<Reservation>().Property(r => r.AptSuite).HasColumnName("apt_suite");
        modelBuilder.Entity<Reservation>().Property(r => r.City).HasColumnName("city");
        modelBuilder.Entity<Reservation>().Property(r => r.State).HasColumnName("state");
        modelBuilder.Entity<Reservation>().Property(r => r.ZipCode).HasColumnName("zip_code");
        modelBuilder.Entity<Reservation>().Property(r => r.RoomType).HasColumnName("room_type");
        modelBuilder.Entity<Reservation>().Property(r => r.RoomFloor).HasColumnName("room_floor");
        modelBuilder.Entity<Reservation>().Property(r => r.RoomNumber).HasColumnName("room_number");
        modelBuilder.Entity<Reservation>().Property(r => r.TotalBill).HasColumnName("total_bill");
        modelBuilder.Entity<Reservation>().Property(r => r.PaymentType).HasColumnName("payment_type");
        modelBuilder.Entity<Reservation>().Property(r => r.CardType).HasColumnName("card_type");
        modelBuilder.Entity<Reservation>().Property(r => r.CardNumber).HasColumnName("card_number");
        modelBuilder.Entity<Reservation>().Property(r => r.CardExp).HasColumnName("card_exp");
        modelBuilder.Entity<Reservation>().Property(r => r.CardCvc).HasColumnName("card_cvc");
        modelBuilder.Entity<Reservation>().Property(r => r.ArrivalTime).HasColumnName("arrival_time");
        modelBuilder.Entity<Reservation>().Property(r => r.LeavingTime).HasColumnName("leaving_time");
        modelBuilder.Entity<Reservation>().Property(r => r.CheckIn).HasColumnName("check_in");
        modelBuilder.Entity<Reservation>().Property(r => r.BreakFast).HasColumnName("break_fast");
        modelBuilder.Entity<Reservation>().Property(r => r.Lunch).HasColumnName("lunch");
        modelBuilder.Entity<Reservation>().Property(r => r.Dinner).HasColumnName("dinner");
        modelBuilder.Entity<Reservation>().Property(r => r.Cleaning).HasColumnName("cleaning");
        modelBuilder.Entity<Reservation>().Property(r => r.Towel).HasColumnName("towel");
        modelBuilder.Entity<Reservation>().Property(r => r.SpecialSurprise).HasColumnName("s_surprise");
        modelBuilder.Entity<Reservation>().Property(r => r.SupplyStatus).HasColumnName("supply_status");
        modelBuilder.Entity<Reservation>().Property(r => r.FoodBill).HasColumnName("food_bill");

        modelBuilder.Entity<Reservation>()
            .Property(r => r.ArrivalTime)
            .HasColumnType("datetime");

        modelBuilder.Entity<Reservation>()
            .Property(r => r.LeavingTime)
            .HasColumnType("datetime");
  

}
}
    


