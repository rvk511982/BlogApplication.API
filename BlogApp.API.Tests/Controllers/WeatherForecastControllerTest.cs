using BlogApp.API.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogApp.API.Tests.Controllers
{
    public class WeatherForecastControllerTest
    {
        private WeatherForecastController? _weatherController;
        private Mock<ILogger<WeatherForecastController>> _mockWeather = new Mock<ILogger<WeatherForecastController>>();

        private void Initilize()
        {
            _weatherController = new WeatherForecastController(_mockWeather.Object);
        }

        [Fact]
        public void Get_ReturnsWeatherForecasts()
        {
            // Arrange
            Initilize();

            // Act
            var result = _weatherController?.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Get_ReturnsWeatherForecasts_NotFound()
        {
            // Arrange
            Initilize();

            // Act
            var result = _weatherController?.Get();

            // Assert
            Assert.NotEqual(MockWeather[0], result?.FirstOrDefault()?.Summary);
        }

        private string[] MockWeather = new[]
        {
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }

}
