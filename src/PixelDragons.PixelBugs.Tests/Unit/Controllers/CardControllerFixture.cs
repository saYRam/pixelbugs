using NUnit.Framework;
using Moq;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using System;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class When_creating_a_new_card : ControllerUnitTestBase
    {
        CardController _controller;
        Mock<ICardService> _cardService;

        [SetUp]
        public void Setup()
        {
            _cardService = new Mock<ICardService>();

            _controller = new CardController(_cardService.Object);
            PrepareController(_controller, "Card");
        }

        [Test]
        public void Should_populate_property_bag_and_render_new_view()
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

            Assert.That(_controller.PropertyBag["users"], Is.EqualTo(users), "The property bag doesn't contain the users list");
            Assert.That(_controller.PropertyBag["types"], Is.EqualTo(types), "The property bag doesn't contain the types list");
            Assert.That(_controller.PropertyBag["statuses"], Is.EqualTo(statuses), "The property bag doesn't contain the statuses list");
            Assert.That(_controller.PropertyBag["priorities"], Is.EqualTo(priorities), "The property bag doesn't contain the priorities list");
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\New"), "The wrong view was rendered");

            _cardService.VerifyAll();
        }

        [Test]
        public void Should_save_the_new_card_and_redirect_to_the_card_wall()
        {
            Card card = new Card();
            User user = new User();

            _cardService.Expect(s => s.SaveCard(card, user)).Returns(card);

            Context.CurrentUser = user;

            _controller.Create(card);

            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"), "The action did not redirect correctly");

            _cardService.VerifyAll();
        }
    }

    [TestFixture]
    public class When_editing_an_existing_card : ControllerUnitTestBase
    {
        CardController _controller;
        Mock<ICardService> _cardService;

        [SetUp]
        public void Setup()
        {
            _cardService = new Mock<ICardService>();

            _controller = new CardController(_cardService.Object);
            PrepareController(_controller, "Card");
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_card_wall_view()
        {
            Card[] cards = new Card[] { };
            CardStatus[] statuses = new CardStatus[] { };

            _cardService.Expect(s => s.GetCards()).Returns(cards);
            _cardService.Expect(s => s.GetCardStatuses()).Returns(statuses);

            _controller.Index();

            Assert.That(_controller.PropertyBag["cards"], Is.EqualTo(cards), "The property bag doesn't contain the cards list");
            Assert.That(_controller.PropertyBag["statuses"], Is.EqualTo(statuses), "The property bag doesn't contain the statuses");
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\Index"), "The wrong view was rendered");

            _cardService.VerifyAll();
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_show_card_view()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};

            _cardService.Expect(s => s.GetCard(id)).Returns(card);

            _controller.Show(id);

            Assert.That(_controller.PropertyBag["card"], Is.EqualTo(card), "The property bag doesn't contain the card");
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\Show"), "The wrong view was rendered");
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_edit_card_view()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};

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

            Assert.That(_controller.PropertyBag["card"], Is.EqualTo(card), "The property bag doesn't contain the card");
            Assert.That(_controller.PropertyBag["users"], Is.EqualTo(users), "The property bag doesn't contain the users list");
            Assert.That(_controller.PropertyBag["types"], Is.EqualTo(types), "The property bag doesn't contain the types list");
            Assert.That(_controller.PropertyBag["statuses"], Is.EqualTo(statuses), "The property bag doesn't contain the statuses list");
            Assert.That(_controller.PropertyBag["priorities"], Is.EqualTo(priorities), "The property bag doesn't contain the priorities list");
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\Edit"), "The wrong view was rendered");

            _cardService.VerifyAll();
        }

        [Test]
        public void Should_save_the_edited_card_and_redirect_to_the_card_wall()
        {
            Card card = new Card {Id = Guid.NewGuid()};
            User user = new User();

            _cardService.Expect(s => s.SaveCard(card, user)).Returns(card);

            Context.CurrentUser = user;

            _controller.Update(card);

            Assert.That(Response.RedirectedTo, Is.EqualTo(String.Format(@"/Card/Show.ashx?Id={0}", card.Id)), "The action did not redirect correctly");

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

            Assert.That(_controller.LayoutName, Is.Null);
            Assert.That(_controller.SelectedViewName, Is.Null);

            _cardService.VerifyAll();
        }

    }
}
