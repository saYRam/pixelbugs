using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers.Impl
{
    public class CardDetailsDTOMapper : ICardDetailsDTOMapper
    {
        private readonly IUserDTOMapper userDTOMapper;
        private readonly ICardTypeDTOMapper cardTypeDTOMapper;
        private readonly ICardStatusDTOMapper cardStatusDTOMapper;
        private readonly ICardPriorityDTOMapper cardPriorityDTOMapper;

        public CardDetailsDTOMapper(IUserDTOMapper userDTOMapper, ICardTypeDTOMapper cardTypeDTOMapper, ICardStatusDTOMapper cardStatusDTOMapper, ICardPriorityDTOMapper cardPriorityDTOMapper)
        {
            this.userDTOMapper = userDTOMapper;
            this.cardTypeDTOMapper = cardTypeDTOMapper;
            this.cardStatusDTOMapper = cardStatusDTOMapper;
            this.cardPriorityDTOMapper = cardPriorityDTOMapper;
        }

        public CardDetailsDTO MapFrom(Card card)
        {
            return new CardDetailsDTO
                       {
                           Id = card.Id,
                           Title = card.Title,
                           Body = card.Body,
                           Number = card.Number,
                           Points = card.Points,
                           Status = cardStatusDTOMapper.MapFrom(card.Status),
                           Priority = cardPriorityDTOMapper.MapFrom(card.Priority),
                           Type = cardTypeDTOMapper.MapFrom(card.Type),
                           Owner = userDTOMapper.MapFrom(card.Owner)
                       };
        }

        public IEnumerable<CardDetailsDTO> MapCollection(Card[] cards)
        {
            foreach (Card card in cards)
            {
                yield return MapFrom(card);
            }
        }
    }
}