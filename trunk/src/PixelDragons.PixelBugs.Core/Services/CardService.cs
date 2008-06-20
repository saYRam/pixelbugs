﻿using System;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.Commons.Repositories;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class CardService : ICardService
    {
        #region Properties
        private IRepository<User> _userRepository;
        private IRepository<Card> _cardRepository;
        private IRepository<CardType> _cardTypeRepository;
        private IRepository<CardStatus> _cardStatusRepository;
        private IRepository<CardPriority> _cardPriorityRepository;
        private ICardStatusQueries _cardStatusQueries;
        private ICardPriorityQueries _cardPriorityQueries;
        #endregion

        #region Constructors
        public CardService(
            IRepository<User> userRepository, 
            IRepository<Card> cardRepository,
            IRepository<CardType> cardTypeRepository,
            IRepository<CardStatus> cardStatusRepository,
            IRepository<CardPriority> cardPriorityRepository,
            ICardStatusQueries cardStatusQueries,
            ICardPriorityQueries cardPriorityQueries)
        {
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _cardTypeRepository = cardTypeRepository;
            _cardStatusRepository = cardStatusRepository;
            _cardPriorityRepository = cardPriorityRepository;
            _cardStatusQueries = cardStatusQueries;
            _cardPriorityQueries = cardPriorityQueries;
        }
        #endregion

        public Card[] GetCards()
        {
            return _cardRepository.FindAll();
        }

        public User[] GetUsersThatCanOwnCards()
        {
            return _userRepository.FindAll();
        }

        public Card SaveCard(Card card, User currentUser)
        {
            if (card.Id == Guid.Empty)
            {
                card.CreatedDate = DateTime.Now;
                card.CreatedBy = currentUser;
            }

            return _cardRepository.Save(card);
        }

        public CardType[] GetCardTypes()
        {
            return _cardTypeRepository.FindAll();
        }

        public CardStatus[] GetCardStatuses()
        {
            DetachedCriteria criteria = _cardStatusQueries.BuildListQuery();
            
            return _cardStatusRepository.FindAll(criteria);
        }

        public CardPriority[] GetCardPriorities()
        {
            DetachedCriteria criteria = _cardPriorityQueries.BuildListQuery();

            return _cardPriorityRepository.FindAll(criteria);
        }

        public Card GetCard(Guid id)
        {
            return _cardRepository.FindById(id);
        }

        public Card ChangeCardStatus(Guid cardId, Guid statusId, User user)
        {
            Card card = _cardRepository.FindById(cardId);
            card.Status = _cardStatusRepository.FindById(statusId);
            
            _cardRepository.Save(card);

            return card;
        }
    }
}
