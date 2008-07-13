namespace PixelDragons.Patterns.Observer
{
    public class MeasurementsChangedEventArgs  
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }

        public MeasurementsChangedEventArgs(float temperature, float humidity, float pressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
        }
    }
}