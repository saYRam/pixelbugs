using System;
using System.Collections.Generic;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class CardMapper : ICardMapper
    {
        private IRepository<Card> cardRepository;
        private IRepository<User> userRepository;
        private IRepository<CardStatus> cardStatusRepository;
        private IRepository<CardPriority> cardPriorityRepository;
        private IRepository<CardType> cardTypeRepository;

        public CardMapper(IRepository<Card> cardRepository, IRepository<User> userRepository, IRepository<CardStatus> cardStatusRepository, IRepository<CardPriority> cardPriorityRepository, IRepository<CardType> cardTypeRepository)
        {
            this.cardRepository = cardRepository;
            this.userRepository = userRepository;
            this.cardStatusRepository = cardStatusRepository;
            this.cardPriorityRepository = cardPriorityRepository;
            this.cardTypeRepository = cardTypeRepository;
        }

        public Card MapFrom(CardDetailsDTO cardDetailsDTO)
        {
            //First, load the card if required
            Card card = new Card();
            if (cardDetailsDTO.Id != Guid.Empty)
            {
                card = cardRepository.FindById(cardDetailsDTO.Id);
            }

            //Now override the values in the domain from the dto but don't merge
            //the number field as this is automatic and not writeable
            card.Title = cardDetailsDTO.Title;
            card.Body = cardDetailsDTO.Body;
            card.Points = cardDetailsDTO.Points;
            
            MapOwner(cardDetailsDTO, card);
            MapStatus(cardDetailsDTO, card);
            MapPriority(cardDetailsDTO, card);
            MapType(cardDetailsDTO, card);
            
            return card;
        }

        private void MapOwner(CardDetailsDTO cardDetailsDTO, Card card)
        {
            if (cardDetailsDTO.Owner != null && cardDetailsDTO.Owner.Id != Guid.Empty)
            {
                card.Owner = userRepository.FindById(cardDetailsDTO.Owner.Id);
            }
            else
            {
                card.Owner = null;
            }
        }

        private void MapStatus(CardDetailsDTO cardDetailsDTO, Card card)
        {
            if (cardDetailsDTO.Status != null && cardDetailsDTO.Status.Id != Guid.Empty)
            {
                card.Status = cardStatusRepository.FindById(cardDetailsDTO.Status.Id);
            }
            else
            {
                card.Status = null;
            }
        }

        private void MapPriority(CardDetailsDTO cardDetailsDTO, Card card)
        {
            if (cardDetailsDTO.Priority != null && cardDetailsDTO.Priority.Id != Guid.Empty)
            {
                card.Priority = cardPriorityRepository.FindById(cardDetailsDTO.Priority.Id);
            }
            else
            {
                card.Priority = null;
            }
        }

        private void MapType(CardDetailsDTO cardDetailsDTO, Card card)
        {
            if (cardDetailsDTO.Type != null && cardDetailsDTO.Type.Id != Guid.Empty)
            {
                card.Type = cardTypeRepository.FindById(cardDetailsDTO.Type.Id);
            }
            else
            {
                card.Type = null;
            }
        }

        public IEnumerable<Card> MapCollection(CardDetailsDTO[] cardDetailsDTOs)
        {
            IList<Card> cards = new List<Card>();
            foreach (CardDetailsDTO cardDetailsDTO in cardDetailsDTOs)
            {
                cards.Add(MapFrom(cardDetailsDTO));
            }

            return cards;
        }
    }
}