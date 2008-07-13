using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace PixelDragons.Patterns.Observer.Tests
{
    [TestFixture]
    public class When_weather_data_changes
    {
        private MockRepository mockery;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
        }

        [Test]
        public void Should_notify_observers()
        {
            IWeatherData weatherData = new WeatherData();

            const float temperature = 32.0f;
            const float humidity = 65.0f;
            const float pressure = 20.0f;

            bool eventHandled = false;

            weatherData.MeasurementsChanged += delegate(object sender, MeasurementsChangedEventArgs measurements)
                                                   {
                                                       Assert.That(measurements.Temperature, Is.EqualTo(temperature));
                                                       Assert.That(measurements.Humidity, Is.EqualTo(humidity));
                                                       Assert.That(measurements.Pressure, Is.EqualTo(pressure));
                                                       eventHandled = true;
                                                   };

            weatherData.SetMeasurements(temperature, humidity, pressure);

            Assert.That(eventHandled, Is.True);
        }

        [Test]
        public void Should_update_current_conditions_display_and_cache_the_readings()
        {
            IConsole console = new DisplayConsole();

            IWeatherData weatherData = mockery.DynamicMock<IWeatherData>();
            weatherData.MeasurementsChanged += null;
            IEventRaiser measurementsChangedEvent = LastCall.IgnoreArguments().GetEventRaiser();
            mockery.ReplayAll();

            IWeatherDisplay currentConditionsDisplay = new CurrentConditionsDisplay(weatherData, console);

            measurementsChangedEvent.Raise(weatherData, new MeasurementsChangedEventArgs(32.0f, 65.0f, 20.0f));
            mockery.VerifyAll();

            Assert.That(console.LastOutput, Is.EqualTo("Current Conditions: 32C degrees and 65% humidity"));
            Assert.That(currentConditionsDisplay.Temperature, Is.EqualTo(32.0f));
            Assert.That(currentConditionsDisplay.Humidity, Is.EqualTo(65.0f));
            Assert.That(currentConditionsDisplay.Pressure, Is.EqualTo(20.0f));
        }
    }
}