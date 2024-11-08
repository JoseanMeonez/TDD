﻿using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain;
using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Core.Processors;

public class RoomBookingRequestProcessor(IRoomBookingService roomBookingService) : IRoomBookingRequestProcessor
{
	public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
	{
		ArgumentNullException.ThrowIfNull(bookingRequest);

		var availableRooms = roomBookingService.GetAvailableRooms(bookingRequest.Date);
		var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

		if (availableRooms.Any())
		{
			var room = availableRooms.First();
			var roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);
			roomBooking.RoomId = room.Id;

			roomBookingService.Save(roomBooking);

			result.RoomBookingId = roomBooking.Id;
			result.Flag = BookingResultFlag.Success;
		}
		else
		{
			result.Flag = BookingResultFlag.Failure;
		}

		return result;
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