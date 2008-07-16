using System;

namespace PixelDragons.PixelBugs.Core.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}