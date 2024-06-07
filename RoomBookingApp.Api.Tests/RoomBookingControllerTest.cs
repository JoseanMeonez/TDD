using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBookingApp.Api.Controllers;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Api.Tests;

public class RoomBookingControllerTest
{
	private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;
	private RoomBookingController _controller;
	private RoomBookingRequest _request;
	private RoomBookingResult _result;

	public RoomBookingControllerTest()
	{
		_roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
		_controller = new RoomBookingController(_roomBookingProcessor.Object);
		_request = new RoomBookingRequest();
		_result = new RoomBookingResult();

		_roomBookingProcessor.Setup(x => x.BookRoom(_request)).Returns(_result);
	}

	[Theory]
	[InlineData(1, true, typeof(OkObjectResult))]
	[InlineData(0, false, typeof(BadRequestObjectResult))]
	public async Task ShouldCallBookingMethodWhenValid(int expectedMethodCalls, bool isModelValid, Type expectedActionResultType)
	{
		// Arrange
		if (!isModelValid)
		{
			_controller.ModelState.AddModelError("Key", "ErrorMessage");
		}

		// Act
		var result = await _controller.BookRoom(_request);

		// Assert
		result.ShouldBeOfType(expectedActionResultType);
		_roomBookingProcessor.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));
	}
}
