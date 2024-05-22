namespace RoomBookingApp.Core.Domain;

public class RoomBookingRequest
{
	public string FullName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public DateTime Date { get; set; }
}