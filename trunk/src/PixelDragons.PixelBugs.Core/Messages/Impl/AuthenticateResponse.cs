using System;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class AuthenticateResponse     
    {
        public Guid Id { get; set; }
        
        public AuthenticateResponse(Guid id)
        {
            Id = id;
        }
    }
}