using System;

namespace PixelDragons.Patterns.Observer
{
    public class DisplayConsole : IConsole
    {
        private string lastOutput;

        public string LastOutput
        {
            get { return lastOutput; }
        }

        public void WriteLine(string message, params object[] args)
        {
            lastOutput = string.Format(message, args);
            Console.WriteLine(lastOutput);
        }
    }
}