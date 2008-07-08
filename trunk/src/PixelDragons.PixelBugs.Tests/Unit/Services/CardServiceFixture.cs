using System;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Queries;
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
        private ICardStatusQueries cardStatusQueries;
        private ICardPriorityQueries cardPriorityQueries;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();

            userRepository = mockery.DynamicMock<IRepository<User>>();
            cardRepository = mockery.DynamicMock<IRepository<Card>>();
            cardTypeRepository = mockery.DynamicMock<IRepository<CardType>>();
            cardStatusRepository = mockery.DynamicMock<IRepository<CardStatus>>();
            cardPriorityRepository = mockery.DynamicMock<IRepository<CardPriority>>();
            cardStatusQueries = mockery.DynamicMock<ICardStatusQueries>();
            cardPriorityQueries = mockery.DynamicMock<ICardPriorityQueries>();

            service = new CardService(userRepository, cardRepository, cardTypeRepository, cardStatusRepository,
                                       cardPriorityRepository, cardStatusQueries, cardPriorityQueries);
        }

        [Test]
        public void Should_be_able_to_retrieve_all_cards()
        {
            Card[] cards = new Card[] {};
            Card[] results;

            using (mockery.Record())
            {
                Expect.Call(cardRepository.FindAll()).Return(cards);
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
            User[] results;

            using (mockery.Record())
            {
                Expect.Call(userRepository.FindAll()).Return(users);
            }

            using (mockery.Playback())
            {
                results = service.GetUsersThatCanOwnCards();
            }

            Assert.That(results, Is.EqualTo(users));
        }

        [Test]
        public void Should_be_able_to_save_a_card()
        {
            Card card = new Card();
            User user = new User();

            using (mockery.Record())
            {
                Expect.Call(cardRepository.Save(card)).Return(card);
            }

            using (mockery.Playback())
            {
                service.SaveCard(card, user);
            }

            Assert.That(card.CreatedBy, Is.EqualTo(user));
            Assert.That(card.CreatedDate.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_types()
        {
            CardType[] types = new CardType[] {};
            CardType[] results;

            using (mockery.Record())
            {
                Expect.Call(cardTypeRepository.FindAll()).Return(types);
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
            DetachedCriteria criteria = DetachedCriteria.For<CardStatus>();

            using (mockery.Record())
            {
                Expect.Call(cardStatusQueries.BuildListQuery()).Return(criteria);
                Expect.Call(cardStatusRepository.FindAll(criteria)).Return(statuses);
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
            DetachedCriteria criteria = DetachedCriteria.For<CardPriority>();

            using (mockery.Record())
            {
                Expect.Call(cardPriorityQueries.BuildListQuery()).Return(criteria);
                Expect.Call(cardPriorityRepository.FindAll(criteria)).Return(priorities);
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
        public void Should_be_able_to_change_a_cards_status()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();
            User user = new User();
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
                result = service.ChangeCardStatus(cardId, statusId, user);
            }

            Assert.That(result.Status, Is.EqualTo(status));
        }
    }
}