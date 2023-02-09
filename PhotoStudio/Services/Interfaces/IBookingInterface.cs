using System.Collections.Generic;
using PhotoStudio.Models.DataBase;

namespace PhotoStudio.Services.Interfaces;

public interface IBookingInterface
{
    public Booking GetBooking(int id);
    public Booking AddBooking(Booking booking);
    public bool EditBooking(Booking booking);
    public bool DeleteBooking(int id);
    public List<Booking> GetAllBookings(Booking booking);
}