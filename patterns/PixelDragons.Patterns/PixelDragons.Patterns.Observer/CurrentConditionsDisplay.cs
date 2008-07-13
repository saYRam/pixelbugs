namespace PixelDragons.Patterns.Observer
{
    public class CurrentConditionsDisplay : IWeatherDisplay
    {
        private readonly IConsole console;

        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }

        public CurrentConditionsDisplay(IWeatherData weatherData, IConsole console)
        {
            this.console = console;

            weatherData.MeasurementsChanged += weatherData_MeasurementsChanged;
        }

        private void weatherData_MeasurementsChanged(object sender, MeasurementsChangedEventArgs eventArgs)
        {
            Temperature = eventArgs.Temperature;
            Pressure = eventArgs.Pressure;
            Humidity = eventArgs.Humidity;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            console.WriteLine("Current Conditions: {0}C degrees and {1}% humidity", Temperature, Humidity);
        }
    }
}