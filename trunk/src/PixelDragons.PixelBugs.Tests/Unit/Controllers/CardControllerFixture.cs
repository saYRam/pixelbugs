using MbUnit.Framework;
using Moq;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using System;

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

        [Test]
        public void Show_Success()
        {
            Guid id = Guid.NewGuid();

            Card card = new Card();
            card.Id = id;

            _cardService.Expect(s => s.GetCard(id)).Returns(card);

            _controller.Show(id);

            Assert.AreEqual(card, _controller.PropertyBag["card"], "The property bag doesn't contain the card");
            Assert.AreEqual(@"Card\Show", _controller.SelectedViewName, "The wrong view was rendered");
        }

        [Test]
        public void Edit_Success()
        {
            Guid id = Guid.NewGuid();

            Card card = new Card();
            card.Id = id;

            User[] users = new User[] { };
            CardType[] types = new CardType[] { };
            CardStatus[] statuses = new CardStatus[] { };
            CardPriority[] priorities = new CardPriority[] { };

            _cardService.Expect(s => s.GetUsersThatCanOwnCards()).Returns(users);
            _cardService.Expect(s => s.GetCardTypes()).Returns(types);
            _cardService.Expect(s => s.GetCardStatuses()).Returns(statuses);
            _cardService.Expect(s => s.GetCardPriorities()).Returns(priorities);
            _cardService.Expect(s => s.GetCard(id)).Returns(card);

            _controller.Edit(id);

            Assert.AreEqual(card, _controller.PropertyBag["card"], "The property bag doesn't contain the card");
            Assert.AreEqual(users, _controller.PropertyBag["users"], "The property bag doesn't contain the users list");
            Assert.AreEqual(types, _controller.PropertyBag["types"], "The property bag doesn't contain the types list");
            Assert.AreEqual(statuses, _controller.PropertyBag["statuses"], "The property bag doesn't contain the statuses list");
            Assert.AreEqual(priorities, _controller.PropertyBag["priorities"], "The property bag doesn't contain the priorities list");
            Assert.AreEqual(@"Card\Edit", _controller.SelectedViewName, "The wrong view was rendered");

            _cardService.VerifyAll();
        }

        [Test]
        public void Update_Success()
        {
            Card card = new Card();
            card.Id = Guid.NewGuid();

            User user = new User();

            _cardService.Expect(s => s.SaveCard(card, user)).Returns(card);

            Context.CurrentUser = user;

            _controller.Update(card);

            Assert.AreEqual(String.Format(@"/Card/Show.ashx?Id={0}", card.Id), Response.RedirectedTo, "The action did not redirect correctly");

            _cardService.VerifyAll();
        }

        [Test]
        public void Should_update_the_card_status_on_an_ajax_call_from_the_client()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();
            User user = new User();

            _cardService.Expect(s => s.ChangeCardStatus(cardId, statusId, user)).Returns(new Card());

            Context.CurrentUser = user;

            _controller.UpdateStatus(cardId, statusId);

            Assert.IsNull(_controller.LayoutName);
            Assert.IsNull(_controller.SelectedViewName);

            _cardService.VerifyAll();
        }

    }
}
