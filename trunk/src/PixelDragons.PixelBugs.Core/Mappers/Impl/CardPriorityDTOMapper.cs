using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers.Impl
{
    public class CardPriorityDTOMapper : ICardPriorityDTOMapper
    {
        public CardPriorityDTO MapFrom(CardPriority cardPriority)
        {
            return new CardPriorityDTO(cardPriority.Id, cardPriority.Name, cardPriority.Colour);
        }

        public IEnumerable<CardPriorityDTO> MapCollection(CardPriority[] cardPriorities)
        {
            foreach (CardPriority cardPriority in cardPriorities)
            {
                yield return MapFrom(cardPriority);
            }
        }
    }
}