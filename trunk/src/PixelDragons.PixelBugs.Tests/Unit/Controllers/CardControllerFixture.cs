using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Web.Controllers;
using System;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class When_creating_a_new_card : ControllerUnitTestBase
    {
        private MockRepository mockery;
        private CardController _controller;
        private ICardService _cardService;
        
        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            _cardService = mockery.DynamicMock<ICardService>();
            
            _controller = new CardController(_cardService);
            PrepareController(_controller, "Card");
        }

        [Test]
        public void Should_populate_property_bag_and_render_new_view()
        { 
            User[] users = new User[] { };
            CardType[] types = new CardType[] { };
            CardStatus[] statuses = new CardStatus[] { };
            CardPriority[] priorities = new CardPriority[] { };

            using (mockery.Record())
            {
                Expect.Call(_cardService.GetUsersThatCanOwnCards()).Return(users);
                Expect.Call(_cardService.GetCardTypes()).Return(types);
                Expect.Call(_cardService.GetCardStatuses()).Return(statuses);
                Expect.Call(_cardService.GetCardPriorities()).Return(priorities);
            }

            using(mockery.Playback())
            {
                _controller.New();
            }
            
            Assert.That(_controller.PropertyBag["users"], Is.EqualTo(users));
            Assert.That(_controller.PropertyBag["types"], Is.EqualTo(types));
            Assert.That(_controller.PropertyBag["statuses"], Is.EqualTo(statuses));
            Assert.That(_controller.PropertyBag["priorities"], Is.EqualTo(priorities));
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\New"));            
        }

        [Test]
        public void Should_save_the_new_card_and_redirect_to_the_card_wall()
        {
            Card card = new Card();
            User user = new User();

            Context.CurrentUser = user;

            using (mockery.Record())
            {
                Expect.Call(_cardService.SaveCard(card, user)).Return(card);
            }

            using (mockery.Playback())
            {
                _controller.Create(card);
            }

            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"));            
        }
    }

    [TestFixture]
    public class When_editing_an_existing_card : ControllerUnitTestBase
    {
        private MockRepository mockery;
        private CardController _controller;
        private ICardService _cardService;

        [SetUp]
        public void Setup()
        {
            mockery = new MockRepository();
            _cardService = mockery.DynamicMock<ICardService>();

            _controller = new CardController(_cardService);
            PrepareController(_controller, "Card");
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_card_wall_view()
        {
            Card[] cards = new Card[] { };
            CardStatus[] statuses = new CardStatus[] { };

            using (mockery.Record())
            {
                Expect.Call(_cardService.GetCards()).Return(cards);
                Expect.Call(_cardService.GetCardStatuses()).Return(statuses);
            }

            using (mockery.Playback())
            {
                _controller.Index();
            }
            
            Assert.That(_controller.PropertyBag["cards"], Is.EqualTo(cards));
            Assert.That(_controller.PropertyBag["statuses"], Is.EqualTo(statuses));
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\Index"));
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_show_card_view()
        {
            Guid id = Guid.NewGuid();
            Card card = new Card {Id = id};

            using (mockery.Record())
            {
                Expect.Call(_cardService.GetCard(id)).Return(card);
            }

            using (mockery.Playback())
            {
                _controller.Show(id);
            }
            
            Assert.That(_controller.PropertyBag["card"], Is.EqualTo(card));
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\Show"));
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

            using (mockery.Record())
            {
                Expect.Call(_cardService.GetUsersThatCanOwnCards()).Return(users);
                Expect.Call(_cardService.GetCardTypes()).Return(types);
                Expect.Call(_cardService.GetCardStatuses()).Return(statuses);
                Expect.Call(_cardService.GetCardPriorities()).Return(priorities);
                Expect.Call(_cardService.GetCard(id)).Return(card);
            }

            using (mockery.Playback())
            {
                _controller.Edit(id);
            }
            
            Assert.That(_controller.PropertyBag["card"], Is.EqualTo(card));
            Assert.That(_controller.PropertyBag["users"], Is.EqualTo(users));
            Assert.That(_controller.PropertyBag["types"], Is.EqualTo(types));
            Assert.That(_controller.PropertyBag["statuses"], Is.EqualTo(statuses));
            Assert.That(_controller.PropertyBag["priorities"], Is.EqualTo(priorities));
            Assert.That(_controller.SelectedViewName, Is.EqualTo(@"Card\Edit"));
        }

        [Test]
        public void Should_save_the_edited_card_and_redirect_to_the_card_wall()
        {
            Card card = new Card {Id = Guid.NewGuid()};
            User user = new User();

            Context.CurrentUser = user;

            using (mockery.Record())
            {
                Expect.Call(_cardService.SaveCard(card, user)).Return(card);
            }

            using (mockery.Playback())
            {
                _controller.Update(card);
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
                Expect.Call(_cardService.ChangeCardStatus(cardId, statusId, user)).Return(new Card());
            }

            using (mockery.Playback())
            {
                _controller.UpdateStatus(cardId, statusId);
            }

            Assert.That(_controller.LayoutName, Is.Null);
            Assert.That(_controller.SelectedViewName, Is.Null);            
        }

    }
}
