﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages.Impl;
using PixelDragons.PixelBugs.Core.Services.Impl;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class When_using_the_card_service
    {
        private MockRepository mockery;
        private CardService service;
        private IRepository<User> userRepository;
        private IRepository<Card> cardRepository;
        private IRepository<CardType> cardTypeRepository;
        private IRepository<CardStatus> cardStatusRepository;
        private IRepository<CardPriority> cardPriorityRepository;
        private IQueryBuilder query;
        private IUserDTOMapper userDTOMapper;
        private ICardTypeDTOMapper cardTypeDTOMapper;
        private ICardStatusDTOMapper cardStatusDTOMapper;
        private ICardPriorityDTOMapper cardPriorityDTOMapper;
        private ICardDTOMapper cardDTOMapper;
        private ICardDetailsDTOMapper cardDetailsDTOMapper;
        private ICardMapper cardMapper;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();

            userRepository = mockery.DynamicMock<IRepository<User>>();
            cardRepository = mockery.DynamicMock<IRepository<Card>>();
            cardTypeRepository = mockery.DynamicMock<IRepository<CardType>>();
            cardStatusRepository = mockery.DynamicMock<IRepository<CardStatus>>();
            cardPriorityRepository = mockery.DynamicMock<IRepository<CardPriority>>();
            query = mockery.DynamicMock<IQueryBuilder>();
            userDTOMapper = mockery.DynamicMock<IUserDTOMapper>();
            cardTypeDTOMapper = mockery.DynamicMock<ICardTypeDTOMapper>();
            cardStatusDTOMapper = mockery.DynamicMock<ICardStatusDTOMapper>();
            cardPriorityDTOMapper = mockery.DynamicMock<ICardPriorityDTOMapper>();
            cardDTOMapper = mockery.DynamicMock<ICardDTOMapper>();
            cardDetailsDTOMapper = mockery.DynamicMock<ICardDetailsDTOMapper>();
            cardMapper = mockery.DynamicMock<ICardMapper>();

            service = new CardService(userRepository, cardRepository, cardTypeRepository,
                                        cardStatusRepository, cardPriorityRepository, 
                                        userDTOMapper, cardTypeDTOMapper, cardStatusDTOMapper,
                                        cardPriorityDTOMapper, cardDTOMapper, cardDetailsDTOMapper, cardMapper);
        }

        [Test]
        public void Should_be_able_to_retrieve_all_cards_and_statuses_for_a_wall()
        {
            Card[] cards = new Card[] {};
            CardStatus[] cardStatuses = new CardStatus[] { };

            IEnumerable<CardDTO> cardDTOs = new List<CardDTO>();
            IEnumerable<CardStatusDTO> cardStatusDTOs = new List<CardStatusDTO>();

            RetrieveWallResponse response;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.Find(query)).Return(cards).IgnoreArguments();
                Expect.Call(cardStatusRepository.Find(query)).Return(cardStatuses).IgnoreArguments();

                Expect.Call(cardDTOMapper.MapCollection(cards)).Return(cardDTOs);
                Expect.Call(cardStatusDTOMapper.MapCollection(cardStatuses)).Return(cardStatusDTOs);
            }

            using (mockery.Playback())
            {
                response = service.RetrieveWall();
            }

            Assert.That(response.Cards, Is.EqualTo(cardDTOs));
            Assert.That(response.CardStatuses, Is.EqualTo(cardStatusDTOs));
        }

        [Test]
        public void Should_be_able_to_retrieve_options_when_creating_a_new_card()
        {
            User[] users = new User[] { };
            CardType[] cardTypes = new CardType[] { };
            CardStatus[] cardStatuses = new CardStatus[] { };
            CardPriority[] cardPriorities = new CardPriority[] { };

            IEnumerable<UserDTO> userDTOs = new List<UserDTO>();
            IEnumerable<CardTypeDTO> cardTypeDTOs = new List<CardTypeDTO>();
            IEnumerable<CardStatusDTO> cardStatusDTOs = new List<CardStatusDTO>();
            IEnumerable<CardPriorityDTO> cardPriorityDTOs = new List<CardPriorityDTO>();

            RetrieveCardOptionsResponse response;

            using (mockery.Record())
            {
                Expect.Call(userRepository.Find(query)).Return(users).IgnoreArguments();
                Expect.Call(cardTypeRepository.Find(query)).Return(cardTypes).IgnoreArguments();
                Expect.Call(cardStatusRepository.Find(query)).Return(cardStatuses).IgnoreArguments();
                Expect.Call(cardPriorityRepository.Find(query)).Return(cardPriorities).IgnoreArguments();

                Expect.Call(userDTOMapper.MapCollection(users)).Return(userDTOs);
                Expect.Call(cardTypeDTOMapper.MapCollection(cardTypes)).Return(cardTypeDTOs);
                Expect.Call(cardStatusDTOMapper.MapCollection(cardStatuses)).Return(cardStatusDTOs);
                Expect.Call(cardPriorityDTOMapper.MapCollection(cardPriorities)).Return(cardPriorityDTOs);
            }

            using (mockery.Playback())
            {
                response = service.RetrieveCardOptions();
            }

            Assert.That(response.Owners, Is.EqualTo(userDTOs));
            Assert.That(response.CardTypes, Is.EqualTo(cardTypeDTOs));
            Assert.That(response.CardStatuses, Is.EqualTo(cardStatusDTOs));
            Assert.That(response.CardPriorities, Is.EqualTo(cardPriorityDTOs));
        }

        [Test]
        public void Should_be_able_to_retrieve_a_card()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};
            CardDetailsDTO cardDetailsDTO = new CardDetailsDTO { Id = id };
            RetrieveCardRequest request = new RetrieveCardRequest(id);
            RetrieveCardResponse response;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(id)).Return(card);
                Expect.Call(cardDetailsDTOMapper.MapFrom(card)).Return(cardDetailsDTO);
            }

            using (mockery.Playback())
            {
                response = service.RetrieveCard(request);
            }

            Assert.That(response.Card, Is.EqualTo(cardDetailsDTO));
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_when_retrieving_a_card_with_an_invalid_request()
        {
            RetrieveCardRequest request = new RetrieveCardRequest(Guid.Empty);
            service.RetrieveCard(request);
        }

        [Test]
        public void Should_be_able_to_save_a_card()
        {
            CardDetailsDTO cardDetailsDTO = new CardDetailsDTO();
            Card card = new Card();
            SaveCardRequest request = new SaveCardRequest(cardDetailsDTO);
            
            using (mockery.Record())
            {
                Expect.Call(cardMapper.MapFrom(cardDetailsDTO)).Return(card);
                Expect.Call(() => cardRepository.Save(card));
            }

            using (mockery.Playback())
            {
                service.SaveCard(request);
            }
        }

        [Test]
        public void Should_be_able_to_change_a_cards_status()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();
            Card card = new Card();
            CardStatus status = new CardStatus();
            ChangeCardStatusRequest request = new ChangeCardStatusRequest(cardId, statusId);

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(cardId)).Return(card);
                Expect.Call(cardStatusRepository.FindById(statusId)).Return(status);
                Expect.Call(() => cardRepository.Save(card));
            }

            using (mockery.Playback())
            {
                service.ChangeCardStatus(request);
            }

            Assert.That(card.Status, Is.EqualTo(status));
        }
    }
}