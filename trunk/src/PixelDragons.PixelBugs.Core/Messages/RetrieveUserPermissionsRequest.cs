using System;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class RetrieveUserPermissionsRequest : IRequest
    {
        public Guid Id { get; set; }

        public RetrieveUserPermissionsRequest(Guid id)
        {
            Id = id;
        }

        public void Validate()
        {
            if(Id.Equals(Guid.Empty))
                throw new InvalidRequestException("The supplied id cannot be Guid.Empty");
        }
    }
}