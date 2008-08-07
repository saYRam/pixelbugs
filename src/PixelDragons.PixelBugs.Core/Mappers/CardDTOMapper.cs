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
            return new CardDTO
                {
                    Id = card.Id, 
                    Title = card.Title, 
                    Number = card.Number, 
                    StatusId = card.Status.Id,
                    TypeColour = card.Type.Colour,
                    PriorityColour = card.Priority.Colour,
                    PriorityName = card.Priority.Name
                };
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