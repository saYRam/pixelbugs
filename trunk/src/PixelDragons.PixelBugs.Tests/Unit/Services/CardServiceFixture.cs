using System;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.Commons.Repositories;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class When_using_the_card_service
    {
        CardService _service;

        Mock<IRepository<User>> _userRepository;
        Mock<IRepository<Card>> _cardRepository;
        Mock<IRepository<CardType>> _cardTypeRepository;
        Mock<IRepository<CardStatus>> _cardStatusRepository;
        Mock<IRepository<CardPriority>> _cardPriorityRepository;
        Mock<ICardStatusQueries> _cardStatusQueries;
        Mock<ICardPriorityQueries> _cardPriorityQueries;

        [SetUp]
        public void TestSetup()
        {
            _userRepository = new Mock<IRepository<User>>();
            _cardRepository = new Mock<IRepository<Card>>();
            _cardTypeRepository = new Mock<IRepository<CardType>>();
            _cardStatusRepository = new Mock<IRepository<CardStatus>>();
            _cardPriorityRepository = new Mock<IRepository<CardPriority>>();
            _cardStatusQueries = new Mock<ICardStatusQueries>();
            _cardPriorityQueries = new Mock<ICardPriorityQueries>();

            _service = new CardService(
                _userRepository.Object, 
                _cardRepository.Object,
                _cardTypeRepository.Object,
                _cardStatusRepository.Object,
                _cardPriorityRepository.Object,
                _cardStatusQueries.Object,
                _cardPriorityQueries.Object
            );
        }

        [Test]
        public void Should_be_able_to_retrieve_all_cards()
        {
            Card[] cards = new Card[] { };
            _cardRepository.Expect(r => r.FindAll()).Returns(cards);

            Assert.That(_service.GetCards(), Is.EqualTo(cards));
        }

        [Test]
        public void Should_be_able_to_retrieve_users_that_can_own_cards()
        {
            User[] users = new User[] { };
            _userRepository.Expect(r => r.FindAll()).Returns(users);

            Assert.That(_service.GetUsersThatCanOwnCards(), Is.EqualTo(users));
        }

        [Test]
        public void Should_be_able_to_save_a_card()
        {
            Card card = new Card();
            User user = new User();

            _cardRepository.Expect(r => r.Save(card)).Returns(card);

            _service.SaveCard(card, user);

            Assert.That(card.CreatedBy, Is.EqualTo(user));
            Assert.That(card.CreatedDate.Date, Is.EqualTo(DateTime.Now.Date));

            _cardRepository.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_types()
        {
            CardType[] types = new CardType[] { };
            _cardTypeRepository.Expect(r => r.FindAll()).Returns(types);

            Assert.That(_service.GetCardTypes(), Is.EqualTo(types));

            _cardTypeRepository.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_statuses()
        {
            CardStatus[] statuses = new CardStatus[] { };
            DetachedCriteria criteria = DetachedCriteria.For<CardStatus>();

            _cardStatusQueries.Expect(q => q.BuildListQuery()).Returns(criteria);
            _cardStatusRepository.Expect(r => r.FindAll(criteria)).Returns(statuses);

            Assert.That(_service.GetCardStatuses(), Is.EqualTo(statuses));

            _cardStatusRepository.VerifyAll();
            _cardStatusQueries.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_retrieve_all_card_priorities()
        {
            CardPriority[] priorities = new CardPriority[] { };
            DetachedCriteria criteria = DetachedCriteria.For<CardPriority>();

            _cardPriorityQueries.Expect(q => q.BuildListQuery()).Returns(criteria);
            _cardPriorityRepository.Expect(r => r.FindAll(criteria)).Returns(priorities);

            Assert.That(_service.GetCardPriorities(), Is.EqualTo(priorities));

            _cardPriorityRepository.VerifyAll();
            _cardPriorityQueries.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_retrieve_a_card_from_its_id()
        {
            Guid id = Guid.NewGuid();
            
            Card card = new Card {Id = id};

            _cardRepository.Expect(r => r.FindById(id)).Returns(card);

            Assert.That(_service.GetCard(id), Is.EqualTo(card));

            _cardRepository.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_change_a_cards_status()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();
            User user = new User();
            Card card = new Card();
            CardStatus status = new CardStatus();

            _cardRepository.Expect(r => r.FindById(cardId)).Returns(card);
            _cardStatusRepository.Expect(r => r.FindById(statusId)).Returns(status);
            _cardRepository.Expect(r => r.Save(card)).Returns(card);

            _service.ChangeCardStatus(cardId, statusId, user);

            _cardStatusRepository.VerifyAll();
            _cardRepository.VerifyAll();
        }
    }
}
