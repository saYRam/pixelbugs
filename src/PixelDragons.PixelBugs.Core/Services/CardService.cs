using System;
using System.Collections.Generic;
using Castle.Services.Transaction;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;
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
        private readonly IRetrievedUserMapper retrievedUserMapper;

        public CardService(
            IRepository<User> userRepository,
            IRepository<Card> cardRepository,
            IRepository<CardType> cardTypeRepository,
            IRepository<CardStatus> cardStatusRepository,
            IRepository<CardPriority> cardPriorityRepository,
            IRetrievedUserMapper retrievedUserMapper)
        {
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.cardTypeRepository = cardTypeRepository;
            this.cardStatusRepository = cardStatusRepository;
            this.cardPriorityRepository = cardPriorityRepository;
            this.retrievedUserMapper = retrievedUserMapper;
        }

        public Card[] GetCards()
        {
            IQueryBuilder query = new RetrieveCardsQuery();

            return cardRepository.Find(query);
        }

        public IEnumerable<RetrieveUserResponse> GetUsersThatCanOwnCards()
        {
            IQueryBuilder query = new RetrieveUsersQuery();
            User[] users = userRepository.Find(query);
            
            return retrievedUserMapper.MapCollection(users);
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
        public Card SaveCard(Card card, Guid userId)
        {
            if (card.Id == Guid.Empty)
            {
                card.CreatedDate = DateTime.Now;
                card.CreatedBy = userRepository.FindById(userId);
            }

            return cardRepository.Save(card);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public Card ChangeCardStatus(Guid cardId, Guid statusId)
        {
            Card card = cardRepository.FindById(cardId);
            card.Status = cardStatusRepository.FindById(statusId);

            cardRepository.Save(card);

            return card;
        }
    }
}