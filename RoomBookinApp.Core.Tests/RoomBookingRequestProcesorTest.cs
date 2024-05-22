using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookinApp.Core.Tests;

public class RoomBookingRequestProcesorTest
{
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

		var processor = new RoomBookingRequestProcessor();

		// Act
		RoomBookingResult result = processor.BookRoom(request);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(request.FullName, result.FullName);
		Assert.Equal(request.Email, result.Email);
		Assert.Equal(request.Date, result.Date);

		result.ShouldNotBeNull();
		result.FullName.ShouldBe(request.FullName);
		result.Email.ShouldBe(request.Email);
		result.Date.ShouldBe(request.Date);
	}
}