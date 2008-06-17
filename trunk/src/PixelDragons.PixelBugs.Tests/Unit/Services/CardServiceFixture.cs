using System;
using MbUnit.Framework;
using Moq;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.Commons.Repositories;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Services
{
    [TestFixture]
    public class CardServiceFixture
    {
        CardService _service;

        Mock<IRepository<User>> _userRepository;
        Mock<IRepository<Card>> _cardRepository;
        Mock<IRepository<CardType>> _cardTypeRepository;
        Mock<IRepository<CardStatus>> _cardStatusRepository;
        Mock<IRepository<CardPriority>> _cardPriorityRepository;
        Mock<ICardStatusQueries> _cardStatusQueries;

        [SetUp]
        public void TestSetup()
        {
            _userRepository = new Mock<IRepository<User>>();
            _cardRepository = new Mock<IRepository<Card>>();
            _cardTypeRepository = new Mock<IRepository<CardType>>();
            _cardStatusRepository = new Mock<IRepository<CardStatus>>();
            _cardPriorityRepository = new Mock<IRepository<CardPriority>>();
            _cardStatusQueries = new Mock<ICardStatusQueries>();

            _service = new CardService(
                _userRepository.Object, 
                _cardRepository.Object,
                _cardTypeRepository.Object,
                _cardStatusRepository.Object,
                _cardPriorityRepository.Object,
                _cardStatusQueries.Object
            );
        }

        [Test]
        public void GetAllCards_Success()
        {
            Card[] cards = new Card[] { };
            _cardRepository.Expect(r => r.FindAll()).Returns(cards);

            Assert.AreEqual(cards, _service.GetCards());
        }

        [Test]
        public void GetUsersThatCanOwnCards_Success()
        {
            User[] users = new User[] { };
            _userRepository.Expect(r => r.FindAll()).Returns(users);

            Assert.AreEqual(users, _service.GetUsersThatCanOwnCards());
        }

        [Test]
        public void SaveCard_NewCard()
        {
            Card card = new Card();
            User user = new User();

            _cardRepository.Expect(r => r.Save(card)).Returns(card);

            Card savedCard = _service.SaveCard(card, user);

            Assert.AreEqual(user, card.CreatedBy);
            Assert.AreEqual(DateTime.Now.Date, card.CreatedDate.Date);

            _cardRepository.VerifyAll();
        }

        [Test]
        public void GetAllCardTypes_Success()
        {
            CardType[] types = new CardType[] { };
            _cardTypeRepository.Expect(r => r.FindAll()).Returns(types);

            Assert.AreEqual(types, _service.GetCardTypes());

            _cardTypeRepository.VerifyAll();
        }

        [Test]
        public void GetAllCardStatuses_Success()
        {
            CardStatus[] statuses = new CardStatus[] { };
            DetachedCriteria criteria = DetachedCriteria.For<CardStatus>();

            _cardStatusQueries.Expect(q => q.BuildListQuery()).Returns(criteria);
            _cardStatusRepository.Expect(r => r.FindAll(criteria)).Returns(statuses);

            Assert.AreEqual(statuses, _service.GetCardStatuses());

            _cardStatusRepository.VerifyAll();
            _cardStatusQueries.VerifyAll();
        }

        [Test]
        public void GetAllCardPriorities_Success()
        {
            CardPriority[] priorities = new CardPriority[] { };
            _cardPriorityRepository.Expect(r => r.FindAll()).Returns(priorities);

            Assert.AreEqual(priorities, _service.GetCardPriorities());

            _cardPriorityRepository.VerifyAll();
        }

        [Test]
        public void GetCard_Success()
        {
            Guid id = Guid.NewGuid();
            
            Card card = new Card();
            card.Id = id;

            _cardRepository.Expect(r => r.FindById(id)).Returns(card);

            Assert.AreEqual(card, _service.GetCard(id));

            _cardRepository.VerifyAll();
        }

        [Test]
        public void ChangeCardStatus_Success()
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
