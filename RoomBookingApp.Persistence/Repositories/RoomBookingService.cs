using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories;
public class RoomBookingService(RoomBookingAppDbContext context) : IRoomBookingService
{
	public IEnumerable<Room> GetAvailableRooms(DateTime date)
	{
		var availableRooms = context.Rooms.Where(r => r.RoomBookings.Any(rb => rb.Date == date) == false).AsEnumerable();

		return availableRooms;
	}

	public void Save(RoomBooking roomBooking)
	{
		context.Add(roomBooking);
		context.SaveChanges();
	}
}
