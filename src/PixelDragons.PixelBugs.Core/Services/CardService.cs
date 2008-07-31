using System;
using System.Collections.Generic;
using Castle.Services.Transaction;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
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
        private readonly IUserDTOMapper userDTOMapper;
        private readonly ICardTypeDTOMapper cardTypeDTOMapper;
        private readonly ICardStatusDTOMapper cardStatusDTOMapper;
        private readonly ICardPriorityDTOMapper cardPriorityDTOMapper;
        private readonly ICardDTOMapper cardDTOMapper;

        public CardService(
            IRepository<User> userRepository,
            IRepository<Card> cardRepository,
            IRepository<CardType> cardTypeRepository,
            IRepository<CardStatus> cardStatusRepository,
            IRepository<CardPriority> cardPriorityRepository,
            IUserDTOMapper userDTOMapper,
            ICardTypeDTOMapper cardTypeDTOMapper,
            ICardStatusDTOMapper cardStatusDTOMapper,
            ICardPriorityDTOMapper cardPriorityDTOMapper,
            ICardDTOMapper cardDTOMapper)
        {
            //TODO: There are too many parameters for this service, consider injecting a service locator
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.cardTypeRepository = cardTypeRepository;
            this.cardStatusRepository = cardStatusRepository;
            this.cardPriorityRepository = cardPriorityRepository;
            this.userDTOMapper = userDTOMapper;
            this.cardTypeDTOMapper = cardTypeDTOMapper;
            this.cardStatusDTOMapper = cardStatusDTOMapper;
            this.cardPriorityDTOMapper = cardPriorityDTOMapper;
            this.cardDTOMapper = cardDTOMapper;
        }

        /// <summary>
        /// Gets all the available options required when creating a new card
        /// </summary>
        /// <returns>Returns a response containing the options</returns>
        public RetrieveCardOptionsResponse RetrieveCardOptions()
        {
            RetrieveCardOptionsResponse response = new RetrieveCardOptionsResponse();

            response.Owners = GetPossibleCardOwners();
            response.CardTypes = GetCardTypes();
            response.CardStatuses = GetCardStatuses();
            response.CardPriorities = GetCardPriorities();

            return response;
        }

        public RetrieveWallResponse RetrieveWall()
        {
            RetrieveWallResponse response = new RetrieveWallResponse();

            response.Cards = GetCardsForWall();
            response.CardStatuses = GetCardStatuses();

            return response;
        }

        private IEnumerable<UserDTO> GetPossibleCardOwners()
        {
            IQueryBuilder query = new RetrieveUsersQuery();
            User[] users = userRepository.Find(query);
            
            return userDTOMapper.MapCollection(users);
        }

        private IEnumerable<CardTypeDTO> GetCardTypes()
        {
            IQueryBuilder query = new RetrieveCardTypesQuery();
            CardType[] cardTypes = cardTypeRepository.Find(query);

            return cardTypeDTOMapper.MapCollection(cardTypes);
        }

        private IEnumerable<CardStatusDTO> GetCardStatuses()
        {
            IQueryBuilder query = new RetrieveCardStatusesQuery();
            CardStatus[] cardStatuses = cardStatusRepository.Find(query);

            return cardStatusDTOMapper.MapCollection(cardStatuses);
        }

        private IEnumerable<CardPriorityDTO> GetCardPriorities()
        {
            IQueryBuilder query = new RetrieveCardPrioritiesQuery();
            CardPriority[] cardPriorities = cardPriorityRepository.Find(query);

            return cardPriorityDTOMapper.MapCollection(cardPriorities);
        }

        private IEnumerable<CardDTO> GetCardsForWall()
        {
            IQueryBuilder query = new RetrieveCardsQuery();
            Card[] cards = cardRepository.Find(query);

            return cardDTOMapper.MapCollection(cards);
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