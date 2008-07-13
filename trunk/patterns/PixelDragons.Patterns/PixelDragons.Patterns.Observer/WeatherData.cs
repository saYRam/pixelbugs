namespace PixelDragons.Patterns.Observer
{
    public class WeatherData : IWeatherData
    {
        public event MeasurementsChangedEventHandler MeasurementsChanged;

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;

            OnMeasurementsChanged();
        }

        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }

        private void OnMeasurementsChanged()
        {
            if (MeasurementsChanged != null)
            {
                MeasurementsChanged(this, new MeasurementsChangedEventArgs(Temperature, Humidity, Pressure));
            }
        }
    }
}