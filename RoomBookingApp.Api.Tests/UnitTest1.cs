using Microsoft.Extensions.Logging;
using Moq;
using RoomBookingApp.Api.Controllers;
using Shouldly;

namespace RoomBookingApp.Api.Tests;

public class UnitTest1
{
	[Fact]
	public void ShouldReturnForecastResults()
	{
		var loggerMock = new Mock<ILogger<WeatherForecastController>>();
		var controller = new WeatherForecastController(loggerMock.Object);

		var result = controller.Get();

		result.Count().ShouldBeGreaterThan(1);
		result.ShouldNotBeNull();
	}
}