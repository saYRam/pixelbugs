using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class RetrieveCardResponse
    {
        private readonly CardDetailsDTO cardDetailsDTO;

        public RetrieveCardResponse(CardDetailsDTO cardDetailsDTO)
        {
            this.cardDetailsDTO = cardDetailsDTO;
        }

        public CardDetailsDTO Card
        {
            get { return cardDetailsDTO; }
        }
    }
}