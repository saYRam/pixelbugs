using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Services;
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
        private IRetrievedUserMapper retrievedUserMapper;

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
            retrievedUserMapper = mockery.DynamicMock<IRetrievedUserMapper>();

            service = new CardService(userRepository, cardRepository, cardTypeRepository,
                                        cardStatusRepository, cardPriorityRepository, retrievedUserMapper);
        }

        [Test]
        public void Should_be_able_to_retrieve_all_cards()
        {
            Card[] cards = new Card[] {};
            Card[] results;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.Find(query)).Return(cards)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                results = service.GetCards();
            }

            Assert.That(results, Is.EqualTo(cards));
        }

        [Test]
        public void Should_be_able_to_retrieve_users_that_can_own_cards()
        {
            User[] users = new User[] {};
            IEnumerable<RetrieveUserResponse> results;
            IEnumerable<RetrieveUserResponse> mappedUsers = new List<RetrieveUserResponse>();

            using (mockery.Record())
            {
                Expect.Call(userRepository.Find(query)).Return(users)
                    .IgnoreArguments();
                Expect.Call(retrievedUserMapper.MapCollection(users)).Return(mappedUsers);
            }

            using (mockery.Playback())
            {
                results = service.GetUsersThatCanOwnCards();
            }

            Assert.That(results, Is.EqualTo(mappedUsers));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_types()
        {
            CardType[] types = new CardType[] { };
            CardType[] results;

            using (mockery.Record())
            {
                Expect.Call(cardTypeRepository.Find(query)).Return(types)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                results = service.GetCardTypes();
            }

            Assert.That(results, Is.EqualTo(types));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_statuses()
        {
            CardStatus[] statuses = new CardStatus[] {};
            CardStatus[] results;

            using (mockery.Record())
            {
                Expect.Call(cardStatusRepository.Find(query)).Return(statuses)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                results = service.GetCardStatuses();
            }

            Assert.That(results, Is.EqualTo(statuses));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_priorities()
        {
            CardPriority[] priorities = new CardPriority[] {};
            CardPriority[] results;

            using (mockery.Record())
            {
                Expect.Call(cardPriorityRepository.Find(query)).Return(priorities)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                results = service.GetCardPriorities();
            }

            Assert.That(results, Is.EqualTo(priorities));
        }

        [Test]
        public void Should_be_able_to_retrieve_a_card_from_its_id()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};
            Card result;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(id)).Return(card);
            }

            using (mockery.Playback())
            {
                result = service.GetCard(id);
            }

            Assert.That(result, Is.EqualTo(card));
        }

        [Test]
        public void Should_be_able_to_save_a_card()
        {
            Guid userId = Guid.NewGuid();
            Card card = new Card();
            User user = new User {Id = userId};

            using (mockery.Record())
            {
                Expect.Call(cardRepository.Save(card)).Return(card);
                Expect.Call(userRepository.FindById(userId)).Return(user);
            }

            using (mockery.Playback())
            {
                service.SaveCard(card, userId);
            }

            Assert.That(card.CreatedDate.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(card.CreatedBy.Id, Is.EqualTo(userId));
        }

        [Test]
        public void Should_be_able_to_change_a_cards_status()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();
            Card card = new Card();
            CardStatus status = new CardStatus();
            Card result;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindById(cardId)).Return(card);
                Expect.Call(cardStatusRepository.FindById(statusId)).Return(status);
                Expect.Call(cardRepository.Save(card)).Return(card);
            }

            using (mockery.Playback())
            {
                result = service.ChangeCardStatus(cardId, statusId);
            }

            Assert.That(result.Status, Is.EqualTo(status));
        }
    }
}