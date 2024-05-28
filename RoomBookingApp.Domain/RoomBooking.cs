using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Domain;

public class RoomBooking : RoomBookingBase
{
	public int Id { get; set; }
	public int RoomId { get; set; }
	public virtual Room? Room { get; set; }
}