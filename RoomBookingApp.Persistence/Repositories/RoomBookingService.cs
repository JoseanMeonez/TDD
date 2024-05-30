using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories;
public class RoomBookingService : IRoomBookingService
{
	private readonly RoomBookingAppDbContext _context;

	public RoomBookingService(RoomBookingAppDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Room> GetAvailableRooms(DateTime date)
	{
		var unAvailableRooms = _context.RoomBookings.Where(rb => rb.Date == date).Select(rb => rb.RoomId).ToList();

		var availableRooms = _context.Rooms.Where(r => unAvailableRooms.Contains(r.Id) == false).ToList();

		return availableRooms;
	}

	public void Save(RoomBooking roomBooking)
	{
		throw new NotImplementedException();
	}
}
