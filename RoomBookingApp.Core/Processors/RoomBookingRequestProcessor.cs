using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;

namespace RoomBookingApp.Core.Processors;

public class RoomBookingRequestProcessor
{
	private readonly IRoomBookingService _roomBookingService;

	public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
	{
		_roomBookingService = roomBookingService;
	}

	public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
	{
		ArgumentNullException.ThrowIfNull(bookingRequest);

		_roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));

		return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
	}

	private static TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest)
		where TRoomBooking : RoomBookingBase, new()
	{
		return new TRoomBooking
		{
			FullName = bookingRequest.FullName,
			Email = bookingRequest.Email,
			Date = bookingRequest.Date,
		};
	}
}