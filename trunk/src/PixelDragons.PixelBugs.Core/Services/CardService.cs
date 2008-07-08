using System;
using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Core.Services
{
    public class CardService : ICardService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Card> cardRepository;
        private readonly IRepository<CardType> cardTypeRepository;
        private readonly IRepository<CardStatus> cardStatusRepository;
        private readonly IRepository<CardPriority> cardPriorityRepository;
        private readonly ICardStatusQueries cardStatusQueries;
        private readonly ICardPriorityQueries cardPriorityQueries;

        public CardService(
            IRepository<User> userRepository,
            IRepository<Card> cardRepository,
            IRepository<CardType> cardTypeRepository,
            IRepository<CardStatus> cardStatusRepository,
            IRepository<CardPriority> cardPriorityRepository,
            ICardStatusQueries cardStatusQueries,
            ICardPriorityQueries cardPriorityQueries)
        {
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.cardTypeRepository = cardTypeRepository;
            this.cardStatusRepository = cardStatusRepository;
            this.cardPriorityRepository = cardPriorityRepository;
            this.cardStatusQueries = cardStatusQueries;
            this.cardPriorityQueries = cardPriorityQueries;
        }

        public Card[] GetCards()
        {
            return cardRepository.FindAll();
        }

        public User[] GetUsersThatCanOwnCards()
        {
            return userRepository.FindAll();
        }

        public Card SaveCard(Card card, User currentUser)
        {
            if (card.Id == Guid.Empty)
            {
                card.CreatedDate = DateTime.Now;
                card.CreatedBy = currentUser;
            }

            return cardRepository.Save(card);
        }

        public CardType[] GetCardTypes()
        {
            return cardTypeRepository.FindAll();
        }

        public CardStatus[] GetCardStatuses()
        {
            DetachedCriteria criteria = cardStatusQueries.BuildListQuery();

            return cardStatusRepository.FindAll(criteria);
        }

        public CardPriority[] GetCardPriorities()
        {
            DetachedCriteria criteria = cardPriorityQueries.BuildListQuery();

            return cardPriorityRepository.FindAll(criteria);
        }

        public Card GetCard(Guid id)
        {
            return cardRepository.FindById(id);
        }

        public Card ChangeCardStatus(Guid cardId, Guid statusId, User user)
        {
            Card card = cardRepository.FindById(cardId);
            card.Status = cardStatusRepository.FindById(statusId);

            cardRepository.Save(card);

            return card;
        }
    }
}