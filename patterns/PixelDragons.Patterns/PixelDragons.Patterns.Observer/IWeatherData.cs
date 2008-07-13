namespace PixelDragons.Patterns.Observer
{
    public interface IWeatherData
    {
        event MeasurementsChangedEventHandler MeasurementsChanged;
        float Temperature { get; set; }
        float Pressure { get; set; }
        float Humidity { get; set; }
        void SetMeasurements(float temperature, float humidity, float pressure);
    }
}