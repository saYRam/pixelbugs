using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class CardTypeDTOMapper : ICardTypeDTOMapper
    {
        public CardTypeDTO MapFrom(CardType cardType)
        {
            return new CardTypeDTO(cardType.Id, cardType.Name, cardType.Colour);
        }

        public IEnumerable<CardTypeDTO> MapCollection(CardType[] cardTypes)
        {
            foreach (CardType cardType in cardTypes)
            {
                yield return MapFrom(cardType);
            }
        }
    }
}