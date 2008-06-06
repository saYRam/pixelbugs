﻿using MbUnit.Framework;
using Moq;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class CardControllerFixture : ControllerUnitTestBase
    {
        CardController _controller;
        Mock<ICardService> _cardService;

        [SetUp]
        public void TestSetup()
        {
            _cardService = new Mock<ICardService>();

            _controller = new CardController(_cardService.Object);
            PrepareController(_controller, "Card");
        }

        [Test]
        public void New_Success()
        { 
            User[] users = new User[] { };
            CardType[] types = new CardType[] { };
            CardStatus[] statuses = new CardStatus[] { };
            CardPriority[] priorities = new CardPriority[] { };

            _cardService.Expect(s => s.GetUsersThatCanOwnCards()).Returns(users);
            _cardService.Expect(s => s.GetCardTypes()).Returns(types);
            _cardService.Expect(s => s.GetCardStatuses()).Returns(statuses);
            _cardService.Expect(s => s.GetCardPriorities()).Returns(priorities);

            _controller.New();

            Assert.AreEqual(users, _controller.PropertyBag["users"], "The property bag doesn't contain the users list");
            Assert.AreEqual(types, _controller.PropertyBag["types"], "The property bag doesn't contain the types list");
            Assert.AreEqual(statuses, _controller.PropertyBag["statuses"], "The property bag doesn't contain the statuses list");
            Assert.AreEqual(priorities, _controller.PropertyBag["priorities"], "The property bag doesn't contain the priorities list");
            Assert.AreEqual(@"Card\New", _controller.SelectedViewName, "The wrong view was rendered");

            _cardService.VerifyAll();
        }

        [Test]
        public void Create_Success()
        {
            Card card = new Card();
            User user = new User();

            _cardService.Expect(s => s.SaveCard(card, user)).Returns(card);

            Context.CurrentUser = user;

            _controller.Create(card);

            Assert.AreEqual(@"/Card/Index.ashx", Response.RedirectedTo, "The action did not redirect correctly");

            _cardService.VerifyAll();
        }

        [Test]
        public void Index_Success()
        {
            Card[] cards = new Card[] { };
            CardStatus[] statuses = new CardStatus[] { };

            _cardService.Expect(s => s.GetCards()).Returns(cards);
            _cardService.Expect(s => s.GetCardStatuses()).Returns(statuses);

            _controller.Index();

            Assert.AreEqual(cards, _controller.PropertyBag["cards"], "The property bag doesn't contain the cards list");
            Assert.AreEqual(statuses, _controller.PropertyBag["statuses"], "The property bag doesn't contain the statuses");
            Assert.AreEqual(@"Card\Index", _controller.SelectedViewName, "The wrong view was rendered");

            _cardService.VerifyAll();
        }
    }
}