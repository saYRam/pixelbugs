using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Tests.Unit.Stubs;
using Rhino.Mocks;
using System;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_card_details_dto_to_a_domain_card
    {
        private CardMapper mapper;

        private StubRepository stubery;
        private MockRepository mockery;
        private CardDetailsDTO cardDetailsDTO;
        private IRepository<Card> cardRepository;
        private IRepository<User> userRepository;
        private IRepository<CardStatus> cardStatusRepository;
        private IRepository<CardPriority> cardPriorityRepository;
        private IRepository<CardType> cardTypeRepository;
        private Card card;
        private User owner;
        private CardStatus cardStatus;
        private CardPriority cardPriority;
        private CardType cardType;

        [SetUp]
        public void Setup()
        {
            stubery = new StubRepository();
            mockery = new MockRepository();

            cardDetailsDTO = stubery.GetStub<CardDetailsDTO>();
            cardRepository = mockery.DynamicMock<IRepository<Card>>();
            userRepository = mockery.DynamicMock<IRepository<User>>();
            cardStatusRepository = mockery.DynamicMock<IRepository<CardStatus>>();
            cardPriorityRepository = mockery.DynamicMock<IRepository<CardPriority>>();
            cardTypeRepository = mockery.DynamicMock<IRepository<CardType>>();

            mapper = new CardMapper(cardRepository, userRepository, cardStatusRepository, cardPriorityRepository, cardTypeRepository);

            card = stubery.GetStub<Card>();
            owner = stubery.GetStub<User>();
            cardStatus = stubery.GetStub<CardStatus>();
            cardPriority = stubery.GetStub<CardPriority>();
            cardType = stubery.GetStub<CardType>();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(cardDetailsDTO.Id)).Return(card);
                Expect.Call(userRepository.FindById(cardDetailsDTO.Owner.Id)).Return(owner);
                Expect.Call(cardStatusRepository.FindById(cardDetailsDTO.Status.Id)).Return(cardStatus);
                Expect.Call(cardPriorityRepository.FindById(cardDetailsDTO.Priority.Id)).Return(cardPriority);
                Expect.Call(cardTypeRepository.FindById(cardDetailsDTO.Type.Id)).Return(cardType);
            }

            using (mockery.Playback())
            {
                card = mapper.MapFrom(cardDetailsDTO);    
            }
            
            AssertCard(card);
        }

        [Test]
        public void Should_map_from_a_single_instance_with_null_child_objects()
        {
            cardDetailsDTO.Owner.Id = Guid.Empty;
            cardDetailsDTO.Status.Id = Guid.Empty;
            cardDetailsDTO.Priority.Id = Guid.Empty;
            cardDetailsDTO.Type.Id = Guid.Empty;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(cardDetailsDTO.Id)).Return(card);
            }

            using (mockery.Playback())
            {
                card = mapper.MapFrom(cardDetailsDTO);
            }

            Assert.That(card.Id, Is.EqualTo(cardDetailsDTO.Id));
            Assert.That(card.Title, Is.EqualTo(cardDetailsDTO.Title));
            Assert.That(card.Body, Is.EqualTo(cardDetailsDTO.Body));
            Assert.That(card.Number, Is.EqualTo(cardDetailsDTO.Number));
            Assert.That(card.Points, Is.EqualTo(cardDetailsDTO.Points));
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            IEnumerable<Card> cards;
            CardDetailsDTO[] cardDetailsDTOs = new [] { cardDetailsDTO };

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(cardDetailsDTO.Id)).Return(card);
                Expect.Call(userRepository.FindById(cardDetailsDTO.Owner.Id)).Return(owner);
                Expect.Call(cardStatusRepository.FindById(cardDetailsDTO.Status.Id)).Return(cardStatus);
                Expect.Call(cardPriorityRepository.FindById(cardDetailsDTO.Priority.Id)).Return(cardPriority);
                Expect.Call(cardTypeRepository.FindById(cardDetailsDTO.Type.Id)).Return(cardType);
            }

            using (mockery.Playback())
            {
                cards = mapper.MapCollection(cardDetailsDTOs);    
            }

            foreach(Card cardToAssert in cards)
            {
                AssertCard(cardToAssert);
            }
        }

        private void AssertCard(Card cardToAssert)
        {
            Assert.That(cardToAssert.Id, Is.EqualTo(cardDetailsDTO.Id));
            Assert.That(cardToAssert.Title, Is.EqualTo(cardDetailsDTO.Title));
            Assert.That(cardToAssert.Body, Is.EqualTo(cardDetailsDTO.Body));
            Assert.That(cardToAssert.Number, Is.EqualTo(cardDetailsDTO.Number));
            Assert.That(cardToAssert.Points, Is.EqualTo(cardDetailsDTO.Points));
            Assert.That(cardToAssert.Owner.Id, Is.EqualTo(cardDetailsDTO.Owner.Id));
            Assert.That(cardToAssert.Status.Id, Is.EqualTo(cardDetailsDTO.Status.Id));
            Assert.That(cardToAssert.Priority.Id, Is.EqualTo(cardDetailsDTO.Priority.Id));
            Assert.That(cardToAssert.Type.Id, Is.EqualTo(cardDetailsDTO.Type.Id));
        }
    }
}