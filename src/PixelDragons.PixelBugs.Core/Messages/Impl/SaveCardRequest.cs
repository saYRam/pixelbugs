using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class SaveCardRequest : IRequest
    {
        public CardDetailsDTO CardDetailsDTO { get; set; }

        public SaveCardRequest(CardDetailsDTO cardDetailsDTO)
        {
            CardDetailsDTO = cardDetailsDTO;
        }

        public void Validate()
        {
            if(CardDetailsDTO == null)
                throw new InvalidRequestException("The SaveCardRequest failed validation as the CardDetailsDTO is null");
        }
    }
}