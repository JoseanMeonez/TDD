using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookinApp.Core.Tests;

public class RoomBookingRequestProcesorTest
{
	private RoomBookingRequestProcessor _processor;
	private RoomBookingRequest _request;

	public RoomBookingRequestProcesorTest()
	{
		// Arrange object
		_processor = new RoomBookingRequestProcessor();

		// Arrange
		_request = new RoomBookingRequest
		{
			FullName = "Test",
			Email = "test@request.com",
			Date = new DateTime(2024, 5, 21),
		};
	}

	[Fact]
	public void ShouldReturnRoomBookingResponseWithRequestValues()
	{
		// Act
		RoomBookingResult result = _processor.BookRoom(_request);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(_request.FullName, result.FullName);
		Assert.Equal(_request.Email, result.Email);
		Assert.Equal(_request.Date, result.Date);

		// Shoudly
		result.ShouldNotBeNull();
		result.FullName.ShouldBe(_request.FullName);
		result.Email.ShouldBe(_request.Email);
		result.Date.ShouldBe(_request.Date);
	}

	[Fact]
	public void ShouldThrowExceptionForNullRequest()
	{
		var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

		exception.ParamName.ShouldBe("bookingRequest");
	}
}