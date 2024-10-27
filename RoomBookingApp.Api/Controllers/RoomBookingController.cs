using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomBookingController(IRoomBookingRequestProcessor roomBookingProcessor) : ControllerBase
{
	[HttpPost]
	public Task<IActionResult> BookRoom(RoomBookingRequest request)
	{
		if (ModelState.IsValid)
		{
			RoomBookingResult result = roomBookingProcessor.BookRoom(request);

			if (result.Flag is Core.Enums.BookingResultFlag.Success)
			{
				return Task.FromResult<IActionResult>(Ok(result));
			}

			ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No rooms available for given date.");
		}

		return Task.FromResult<IActionResult>(BadRequest(ModelState));
	}
}
