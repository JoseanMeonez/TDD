using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookinApp.Core.Tests;

public class RoomBookingRequestProcesorTest
{
	private RoomBookingRequestProcessor _processor;

	public RoomBookingRequestProcesorTest()
	{
		// Arrange object
		_processor = new RoomBookingRequestProcessor();
	}

	[Fact]
	public void ShouldReturnRoomBookingResponseWithRequestValues()
	{
		// Arrange
		var request = new RoomBookingRequest
		{
			FullName = "Test",
			Email = "test@request.com",
			Date = new DateTime(2024, 5, 21),
		};

		// Act
		RoomBookingResult result = _processor.BookRoom(request);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(request.FullName, result.FullName);
		Assert.Equal(request.Email, result.Email);
		Assert.Equal(request.Date, result.Date);

		// Shoudly
		result.ShouldNotBeNull();
		result.FullName.ShouldBe(request.FullName);
		result.Email.ShouldBe(request.Email);
		result.Date.ShouldBe(request.Date);
	}

	[Fact]
	public void ShouldThrowExceptionForNullRequest()
	{
		var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

		exception.ParamName.ShouldBe("bookingRequest");
	}
}