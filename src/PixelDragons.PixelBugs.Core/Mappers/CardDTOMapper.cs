using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class CardDTOMapper : ICardDTOMapper
    {
        public CardDTO MapFrom(Card card)
        {
            return new CardDTO(card.Id, card.Title, card.Number, card.Status.Id, card.Type.Colour, card.Priority.Colour, card.Priority.Name);
        }

        public IEnumerable<CardDTO> MapCollection(Card[] cards)
        {
            foreach (Card card in cards)
            {
                yield return MapFrom(card);
            }
        }
    }
}