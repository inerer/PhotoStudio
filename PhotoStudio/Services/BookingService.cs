using System.Collections.Generic;
using PhotoStudio.DataBase.Repositories;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.Services;

public class BookingService:IBookingInterface
{
    private readonly BookingRepository _bookingRepository;
    
    public BookingService()
    {
        _bookingRepository = new BookingRepository();
    }

    public Booking GetBooking(int id)
    {
        throw new System.NotImplementedException();
    }

    public Booking AddBooking(Booking booking)
    {
        return _bookingRepository.AddBooking(booking);
    }

    public bool EditBooking(Booking booking)
    {
        throw new System.NotImplementedException();
    }

    public bool DeleteBooking(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<Booking> GetAllBookings(Booking booking)
    {
        return _bookingRepository.GetAllBookings(booking);
    }
}