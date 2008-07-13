namespace PixelDragons.Patterns.Observer
{
    public interface IWeatherDisplay
    {
        float Temperature { get; set; }
        float Pressure { get; set; }
        float Humidity { get; set; }

        void UpdateDisplay();
    }
}