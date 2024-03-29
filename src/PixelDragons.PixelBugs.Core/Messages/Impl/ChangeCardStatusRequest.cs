using System;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class ChangeCardStatusRequest : IRequest 
    {
        public Guid CardId { get; set; }
        public Guid StatusId { get; set; }

        public ChangeCardStatusRequest(Guid cardId, Guid statusId)
        {
            CardId = cardId;
            StatusId = statusId;
        }

        public void Validate()
        {
            if (CardId == Guid.Empty)
                throw new InvalidRequestException("This is an invalid request as no card id was supplied");

            if (StatusId == Guid.Empty)
                throw new InvalidRequestException("This is an invalid request as no status id was supplied");
        }
    }
}