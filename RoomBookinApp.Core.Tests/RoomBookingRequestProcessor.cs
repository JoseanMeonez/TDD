

namespace RoomBookinApp.Core.Tests;

internal class RoomBookingRequestProcessor
{
	internal RoomBookingRequestProcessor()
	{

	}

	internal RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
	{
		return new RoomBookingResult
		{
			FullName = bookingRequest.FullName,
			Email = bookingRequest.Email,
			Date = bookingRequest.Date,
		};
	}
}