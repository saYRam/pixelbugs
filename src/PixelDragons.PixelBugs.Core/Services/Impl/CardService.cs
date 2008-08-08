using System.Collections.Generic;
using Castle.Services.Transaction;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages.Impl;
using PixelDragons.PixelBugs.Core.Queries.CardPriorities;
using PixelDragons.PixelBugs.Core.Queries.Cards;
using PixelDragons.PixelBugs.Core.Queries.CardStatuses;
using PixelDragons.PixelBugs.Core.Queries.CardTypes;
using PixelDragons.PixelBugs.Core.Queries.Users;

namespace PixelDragons.PixelBugs.Core.Services.Impl
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
        private readonly ICardDetailsDTOMapper cardDetailsDTOMapper;
        private ICardMapper cardMapper;

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
            ICardDTOMapper cardDTOMapper,
            ICardDetailsDTOMapper cardDetailsDTOMapper,
            ICardMapper cardMapper)
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
            this.cardDetailsDTOMapper = cardDetailsDTOMapper;
            this.cardMapper = cardMapper;
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

        /// <summary>
        /// Gets all cards for the card wall together with all status lanes
        /// </summary>
        /// <returns>Returns a response containing the wall information</returns>
        public RetrieveWallResponse RetrieveWall()
        {
            RetrieveWallResponse response = new RetrieveWallResponse();

            response.Cards = GetCardsForWall();
            response.CardStatuses = GetCardStatuses();

            return response;
        }
        
        /// <summary>
        /// Gets a card with the given identifier
        /// </summary>
        /// <param name="request">The request that contains the id of the card to get</param>
        /// <returns>Returns the matching card</returns>
        public RetrieveCardResponse RetrieveCard(RetrieveCardRequest request)
        {
            request.Validate();

            Card card = cardRepository.FindById(request.CardId);
            CardDetailsDTO cardDetailsDTO = cardDetailsDTOMapper.MapFrom(card);

            return new RetrieveCardResponse(cardDetailsDTO);
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
        
        [Transaction(TransactionMode.RequiresNew)]
        public void SaveCard(SaveCardRequest request)
        {
            request.Validate();

            Card card = cardMapper.MapFrom(request.CardDetailsDTO);

            cardRepository.Save(card);
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void ChangeCardStatus(ChangeCardStatusRequest request)
        {
            request.Validate();

            Card card = cardRepository.FindById(request.CardId);
            card.Status = cardStatusRepository.FindById(request.StatusId);

            cardRepository.Save(card);
        }
    }
}