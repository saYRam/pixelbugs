namespace PixelDragons.Patterns.Observer
{
    public interface IConsole 
    {
        string LastOutput { get; }
        void WriteLine(string message, params object[] args);
    }
}