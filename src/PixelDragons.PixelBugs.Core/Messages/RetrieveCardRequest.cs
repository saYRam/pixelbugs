using System;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class RetrieveCardRequest : IRequest
    {
        private readonly Guid cardId;

        public RetrieveCardRequest(Guid cardId)
        {
            this.cardId = cardId;
        }

        public Guid CardId
        {
            get { return cardId; }
        }

        public void Validate()
        {
            if(cardId == Guid.Empty)
                throw new InvalidRequestException("RetrieveCardRequest validation failed because the card id is invalid, it cannot be Guid.Empty");
        }
    }
}