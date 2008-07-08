using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.Commons.Repositories;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Queries;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class When_using_the_card_service
    {
        private MockRepository mockery;
        private CardService _service;
        private IRepository<User> _userRepository;
        private IRepository<Card> _cardRepository;
        private IRepository<CardType> _cardTypeRepository;
        private IRepository<CardStatus> _cardStatusRepository;
        private IRepository<CardPriority> _cardPriorityRepository;
        private ICardStatusQueries _cardStatusQueries;
        private ICardPriorityQueries _cardPriorityQueries;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();

            _userRepository = mockery.DynamicMock<IRepository<User>>();
            _cardRepository = mockery.DynamicMock<IRepository<Card>>();
            _cardTypeRepository = mockery.DynamicMock<IRepository<CardType>>();
            _cardStatusRepository = mockery.DynamicMock<IRepository<CardStatus>>();
            _cardPriorityRepository = mockery.DynamicMock<IRepository<CardPriority>>();
            _cardStatusQueries = mockery.DynamicMock<ICardStatusQueries>();
            _cardPriorityQueries = mockery.DynamicMock<ICardPriorityQueries>();

            _service = new CardService(_userRepository, _cardRepository, _cardTypeRepository, _cardStatusRepository,
                                       _cardPriorityRepository, _cardStatusQueries, _cardPriorityQueries);
        }

        [Test]
        public void Should_be_able_to_retrieve_all_cards()
        {
            Card[] cards = new Card[] { };
            Card[] results;

            using (mockery.Record())
            {
                Expect.Call(_cardRepository.FindAll()).Return(cards);
            }

            using (mockery.Playback())
            {
                results = _service.GetCards();
            }

            Assert.That(results, Is.EqualTo(cards));
        }

        [Test]
        public void Should_be_able_to_retrieve_users_that_can_own_cards()
        {
            User[] users = new User[] { };
            User[] results;

            using (mockery.Record())
            {
                Expect.Call(_userRepository.FindAll()).Return(users);
            }

            using (mockery.Playback())
            {
                results = _service.GetUsersThatCanOwnCards();
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
                Expect.Call(_cardRepository.Save(card)).Return(card);
            }

            using (mockery.Playback())
            {
                _service.SaveCard(card, user);
            }
            
            Assert.That(card.CreatedBy, Is.EqualTo(user));
            Assert.That(card.CreatedDate.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_types()
        {
            CardType[] types = new CardType[] { };
            CardType[] results;

            using (mockery.Record())
            {
                Expect.Call(_cardTypeRepository.FindAll()).Return(types);
            }

            using (mockery.Playback())
            {
                results = _service.GetCardTypes();
            }

            Assert.That(results, Is.EqualTo(types));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_statuses()
        {
            CardStatus[] statuses = new CardStatus[] { };
            CardStatus[] results;
            DetachedCriteria criteria = DetachedCriteria.For<CardStatus>();
            
            using (mockery.Record())
            {
                Expect.Call(_cardStatusQueries.BuildListQuery()).Return(criteria);
                Expect.Call(_cardStatusRepository.FindAll(criteria)).Return(statuses);
            }

            using (mockery.Playback())
            {
                results = _service.GetCardStatuses();
            }

            Assert.That(results, Is.EqualTo(statuses));
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_priorities()
        {
            CardPriority[] priorities = new CardPriority[] { };
            CardPriority[] results;
            DetachedCriteria criteria = DetachedCriteria.For<CardPriority>();

            using (mockery.Record())
            {
                Expect.Call(_cardPriorityQueries.BuildListQuery()).Return(criteria);
                Expect.Call(_cardPriorityRepository.FindAll(criteria)).Return(priorities);
            }

            using (mockery.Playback())
            {
                results = _service.GetCardPriorities();
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
                Expect.Call(_cardRepository.FindById(id)).Return(card);
            }

            using (mockery.Playback())
            {
                result = _service.GetCard(id);
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
                Expect.Call(_cardRepository.FindById(cardId)).Return(card);
                Expect.Call(_cardStatusRepository.FindById(statusId)).Return(status);
                Expect.Call(_cardRepository.Save(card)).Return(card);
            }

            using (mockery.Playback())
            {
                result = _service.ChangeCardStatus(cardId, statusId, user);
            }
            
            Assert.That(result.Status, Is.EqualTo(status));
        }
    }
}
