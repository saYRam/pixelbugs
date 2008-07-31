using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class CardStatusDTOMapper : ICardStatusDTOMapper
    {
        public CardStatusDTO MapFrom(CardStatus cardStatus)
        {
            return new CardStatusDTO(cardStatus.Id, cardStatus.Name);
        }

        public IEnumerable<CardStatusDTO> MapCollection(CardStatus[] cardStatuses)
        {
            foreach (CardStatus cardStatus in cardStatuses)
            {
                yield return MapFrom(cardStatus);
            }
        }
    }
}