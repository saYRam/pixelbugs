﻿using System;
using Castle.Services.Transaction;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Queries.CardPriorities;
using PixelDragons.PixelBugs.Core.Queries.Cards;
using PixelDragons.PixelBugs.Core.Queries.CardStatuses;
using PixelDragons.PixelBugs.Core.Queries.CardTypes;
using PixelDragons.PixelBugs.Core.Queries.Users;

namespace PixelDragons.PixelBugs.Core.Services
{
    [Transactional]
    public class CardService : ICardService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Card> cardRepository;
        private readonly IRepository<CardType> cardTypeRepository;
        private readonly IRepository<CardStatus> cardStatusRepository;
        private readonly IRepository<CardPriority> cardPriorityRepository;
        
        public CardService(
            IRepository<User> userRepository,
            IRepository<Card> cardRepository,
            IRepository<CardType> cardTypeRepository,
            IRepository<CardStatus> cardStatusRepository,
            IRepository<CardPriority> cardPriorityRepository)
        {
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.cardTypeRepository = cardTypeRepository;
            this.cardStatusRepository = cardStatusRepository;
            this.cardPriorityRepository = cardPriorityRepository;
        }

        public Card[] GetCards()
        {
            IQueryBuilder query = new RetrieveCardsQuery();

            return cardRepository.Find(query);
        }

        public User[] GetUsersThatCanOwnCards()
        {
            IQueryBuilder query = new RetrieveUsersQuery();

            return userRepository.Find(query);
        }

        public CardType[] GetCardTypes()
        {
            IQueryBuilder query = new RetrieveCardTypesQuery();

            return cardTypeRepository.Find(query);
        }

        public CardStatus[] GetCardStatuses()
        {
            IQueryBuilder query = new RetrieveCardStatusesQuery();

            return cardStatusRepository.Find(query);
        }

        public CardPriority[] GetCardPriorities()
        {
            IQueryBuilder query = new RetrieveCardPrioritiesQuery();

            return cardPriorityRepository.Find(query);
        }

        public Card GetCard(Guid id)
        {
            return cardRepository.FindById(id);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public Card SaveCard(Card card, User currentUser)
        {
            if (card.Id == Guid.Empty)
            {
                card.CreatedDate = DateTime.Now;
                card.CreatedBy = currentUser;
            }

            return cardRepository.Save(card);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public Card ChangeCardStatus(Guid cardId, Guid statusId, User user)
        {
            Card card = cardRepository.FindById(cardId);
            card.Status = cardStatusRepository.FindById(statusId);

            cardRepository.Save(card);

            return card;
        }
    }
}