using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookinApp.Core.Tests;

public class RoomBookingRequestProcesorTest
{
	private RoomBookingRequestProcessor _processor;
	private RoomBookingRequest _request;
	private Mock<IRoomBookingService> _roomBookingServiceMock;

	public RoomBookingRequestProcesorTest()
	{
		// Arrange
		_request = new RoomBookingRequest
		{
			FullName = "Test",
			Email = "test@request.com",
			Date = new DateTime(2024, 5, 21),
		};

		_roomBookingServiceMock = new Mock<IRoomBookingService>();
		_processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
	}

	[Fact]
	public void ShouldReturnRoomBookingResponseWithRequestValues()
	{
		// Act
		RoomBookingResult result = _processor.BookRoom(_request);

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

	[Fact]
	public void ShouldSaveRoomBookingRequest()
	{
		RoomBooking savedBooking = null;
		_roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
			.Callback<RoomBooking>(booking =>
			{
				savedBooking = booking;
			});

		_processor.BookRoom(_request);

		_roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

		savedBooking.ShouldNotBeNull();
		savedBooking.FullName.ShouldBe(_request.FullName);
		savedBooking.Email.ShouldBe(_request.Email);
		savedBooking.Date.ShouldBe(_request.Date);
	}
}