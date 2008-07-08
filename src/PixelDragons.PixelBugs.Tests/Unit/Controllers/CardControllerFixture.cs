using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class When_creating_a_new_card : ControllerUnitTestBase
    {
        private MockRepository mockery;
        private CardController controller;
        private ICardService cardService;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            cardService = mockery.DynamicMock<ICardService>();

            controller = new CardController(cardService);
            PrepareController(controller, "Card");
        }

        [Test]
        public void Should_populate_property_bag_and_render_new_view()
        {
            User[] users = new User[] {};
            CardType[] types = new CardType[] {};
            CardStatus[] statuses = new CardStatus[] {};
            CardPriority[] priorities = new CardPriority[] {};

            using (mockery.Record())
            {
                Expect.Call(cardService.GetUsersThatCanOwnCards()).Return(users);
                Expect.Call(cardService.GetCardTypes()).Return(types);
                Expect.Call(cardService.GetCardStatuses()).Return(statuses);
                Expect.Call(cardService.GetCardPriorities()).Return(priorities);
            }

            using (mockery.Playback())
            {
                controller.New();
            }

            Assert.That(controller.PropertyBag["users"], Is.EqualTo(users));
            Assert.That(controller.PropertyBag["types"], Is.EqualTo(types));
            Assert.That(controller.PropertyBag["statuses"], Is.EqualTo(statuses));
            Assert.That(controller.PropertyBag["priorities"], Is.EqualTo(priorities));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\New"));
        }

        [Test]
        public void Should_save_the_new_card_and_redirect_to_the_card_wall()
        {
            Card card = new Card();
            User user = new User();

            Context.CurrentUser = user;

            using (mockery.Record())
            {
                Expect.Call(cardService.SaveCard(card, user)).Return(card);
            }

            using (mockery.Playback())
            {
                controller.Create(card);
            }

            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"));
        }
    }

    [TestFixture]
    public class When_editing_an_existing_card : ControllerUnitTestBase
    {
        private MockRepository mockery;
        private CardController controller;
        private ICardService cardService;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            cardService = mockery.DynamicMock<ICardService>();

            controller = new CardController(cardService);
            PrepareController(controller, "Card");
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_card_wall_view()
        {
            Card[] cards = new Card[] {};
            CardStatus[] statuses = new CardStatus[] {};

            using (mockery.Record())
            {
                Expect.Call(cardService.GetCards()).Return(cards);
                Expect.Call(cardService.GetCardStatuses()).Return(statuses);
            }

            using (mockery.Playback())
            {
                controller.Index();
            }

            Assert.That(controller.PropertyBag["cards"], Is.EqualTo(cards));
            Assert.That(controller.PropertyBag["statuses"], Is.EqualTo(statuses));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\Index"));
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_show_card_view()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};

            using (mockery.Record())
            {
                Expect.Call(cardService.GetCard(id)).Return(card);
            }

            using (mockery.Playback())
            {
                controller.Show(id);
            }

            Assert.That(controller.PropertyBag["card"], Is.EqualTo(card));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\Show"));
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_edit_card_view()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};

            User[] users = new User[] {};
            CardType[] types = new CardType[] {};
            CardStatus[] statuses = new CardStatus[] {};
            CardPriority[] priorities = new CardPriority[] {};

            using (mockery.Record())
            {
                Expect.Call(cardService.GetUsersThatCanOwnCards()).Return(users);
                Expect.Call(cardService.GetCardTypes()).Return(types);
                Expect.Call(cardService.GetCardStatuses()).Return(statuses);
                Expect.Call(cardService.GetCardPriorities()).Return(priorities);
                Expect.Call(cardService.GetCard(id)).Return(card);
            }

            using (mockery.Playback())
            {
                controller.Edit(id);
            }

            Assert.That(controller.PropertyBag["card"], Is.EqualTo(card));
            Assert.That(controller.PropertyBag["users"], Is.EqualTo(users));
            Assert.That(controller.PropertyBag["types"], Is.EqualTo(types));
            Assert.That(controller.PropertyBag["statuses"], Is.EqualTo(statuses));
            Assert.That(controller.PropertyBag["priorities"], Is.EqualTo(priorities));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\Edit"));
        }

        [Test]
        public void Should_save_the_edited_card_and_redirect_to_the_card_wall()
        {
            Card card = new Card {Id = Guid.NewGuid()};
            User user = new User();

            Context.CurrentUser = user;

            using (mockery.Record())
            {
                Expect.Call(cardService.SaveCard(card, user)).Return(card);
            }

            using (mockery.Playback())
            {
                controller.Update(card);
            }

            Assert.That(Response.RedirectedTo, Is.EqualTo(String.Format(@"/Card/Show.ashx?Id={0}", card.Id)));
        }

        [Test]
        public void Should_update_the_card_status_on_an_ajax_call_from_the_client()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();
            User user = new User();

            Context.CurrentUser = user;

            using (mockery.Record())
            {
                Expect.Call(cardService.ChangeCardStatus(cardId, statusId, user)).Return(new Card());
            }

            using (mockery.Playback())
            {
                controller.UpdateStatus(cardId, statusId);
            }

            Assert.That(controller.LayoutName, Is.Null);
            Assert.That(controller.SelectedViewName, Is.Null);
        }
    }
}