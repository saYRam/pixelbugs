using System;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class ChangeCardStatusRequest : IRequest 
    {
        public ChangeCardStatusRequest()
        {
        }

        public ChangeCardStatusRequest(Guid cardId, Guid statusId)
        {
            CardId = cardId;
            StatusId = statusId;
        }

        public Guid CardId { get; set; }
        public Guid StatusId { get; set; }

        public void Validate()
        {
            if (CardId == Guid.Empty)
                throw new InvalidRequestException("This is an invalid request as no card id was supplied");

            if (StatusId == Guid.Empty)
                throw new InvalidRequestException("This is an invalid request as no status id was supplied");
        }
    }
}