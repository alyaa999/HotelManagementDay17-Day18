using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_Manager.DbContext
{
    class ResrvationRepo
    {
        private DbManager _context;

        public ResrvationRepo()
        {
            _context = new DbManager();
        }

        // Add a new reservation
        public int AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation.Id;
        }

        // Get all reservations
        public List<Reservation> GetAllReservations()
        {
            return _context.Reservations.ToList();
        }

        // Get a reservation by ID
        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.Find(id);
        }

        // Get a reservation by room number
        public Reservation GetReservationByRoomNumber(string roomNumber)
        {
            return _context.Reservations.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        // Get reservations by room number (where CheckIn is true)
        public List<Reservation> GetReservationByRoomNumber()
        {
            return _context.Reservations.Where(r => r.CheckIn == true).ToList();
        }

        // Search reservations by various fields
        public List<Reservation> GetReservationBySearch(string search)
        {
            return _context.Reservations
                .Where(r => r.Id.ToString().Contains(search) ||
                            r.LastName.Contains(search) ||
                            r.FirstName.Contains(search) ||
                            r.Gender.Contains(search) ||
                            r.State.Contains(search) ||
                            r.RoomNumber.Contains(search) ||
                            r.RoomType.Contains(search) ||
                            r.EmailAddress.Contains(search) ||
                            r.PhoneNumber.Contains(search))
                .ToList();
        }

        // Get reservations by supply status (where CheckIn is true and SupplyStatus is false)
        public List<Reservation> GetReservationBySupplyStatus()
        {
            return _context.Reservations
                .Where(r => r.CheckIn == true && r.SupplyStatus == false)
                .ToList();
        }

        // Update reservation supply status and related fields
        public void UpdateReservationSupplyStatus(int id, double totalBill, int breakfast, int lunch, int dinner, bool supplyStatus, bool cleaning, bool towel, bool surprise, int foodBill)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation != null)
            {
                reservation.TotalBill = totalBill;
                reservation.BreakFast = breakfast;
                reservation.Lunch = lunch;
                reservation.Dinner = dinner;
                reservation.SupplyStatus = supplyStatus;
                reservation.Cleaning = cleaning;
                reservation.Towel = towel;
                reservation.SpecialSurprise = surprise;
                reservation.FoodBill = foodBill;

                _context.Reservations.Update(reservation);
                _context.SaveChanges();
            }
        }

        // Get occupied reservations based on CheckIn status
        public List<Reservation> GetOccupiedReservation(bool checkIn)
        {
            return _context.Reservations.Where(r => r.CheckIn == checkIn).ToList();
        }

        // Update a reservation
        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        // Delete a reservation by ID
        public bool DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return true;
        }
    }
}