using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _dbContext;

        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _dbContext = hotelDbContext;
        }

        public async Task<Domain.Entities.Booking> CreateBooking(Domain.Entities.Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public Task<Domain.Entities.Booking> Get(int id)
        {
            return _dbContext.Bookings.Include(b => b.Guest).Include(b => b.Room).Where(x => x.Id == id).FirstAsync();
        }
    }
}
