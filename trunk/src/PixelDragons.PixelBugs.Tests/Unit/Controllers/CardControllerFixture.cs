using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.TestSupport;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Messages.Impl;
using PixelDragons.PixelBugs.Core.Services;
using PixelDragons.PixelBugs.Tests.Unit.Stubs;
using PixelDragons.PixelBugs.Web.Controllers;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Controllers
{
    [TestFixture]
    public class When_creating_a_new_card : ControllerUnitTestBase
    {
        private StubRepository stubery;
        private MockRepository mockery;
        private CardController controller;
        private ICardService cardService;

        [SetUp]
        public void Setup()
        {
            stubery = new StubRepository();
            mockery = new MockRepository();
            cardService = mockery.DynamicMock<ICardService>();

            controller = new CardController(cardService);
            PrepareController(controller, "Card");
        }

        [Test]
        public void Should_populate_property_bag_and_render_new_view()
        {
            RetrieveCardOptionsResponse response = new RetrieveCardOptionsResponse();
            response.Owners = new List<UserDTO>();
            response.CardTypes = new List<CardTypeDTO>();
            response.CardStatuses = new List<CardStatusDTO>();
            response.CardPriorities = new List<CardPriorityDTO>();

            using (mockery.Record())
            {
                Expect.Call(cardService.RetrieveCardOptions()).Return(response);
            }

            using (mockery.Playback())
            {
                controller.New();
            }

            Assert.That(controller.PropertyBag["owners"], Is.EqualTo(response.Owners));
            Assert.That(controller.PropertyBag["types"], Is.EqualTo(response.CardTypes));
            Assert.That(controller.PropertyBag["statuses"], Is.EqualTo(response.CardStatuses));
            Assert.That(controller.PropertyBag["priorities"], Is.EqualTo(response.CardPriorities));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\New"));
        }

        [Test]
        public void Should_save_the_new_card_and_redirect_to_the_card_wall()
        {
            CardDetailsDTO cardDetailsDTO = stubery.GetStub<CardDetailsDTO>();

            using (mockery.Record())
            {
                Expect.Call(() => cardService.SaveCard(null)).IgnoreArguments();
            }

            using (mockery.Playback())
            {
                controller.Create(cardDetailsDTO);
            }

            Assert.That(Response.RedirectedTo, Is.EqualTo(@"/Card/Index.ashx"));
        }
    }

    [TestFixture]
    public class When_editing_an_existing_card : ControllerUnitTestBase
    {
        private StubRepository stubery;
        private MockRepository mockery;
        private CardController controller;
        private ICardService cardService;

        [SetUp]
        public void Setup()
        {
            stubery = new StubRepository();
            mockery = new MockRepository();
            cardService = mockery.DynamicMock<ICardService>();

            controller = new CardController(cardService);
            PrepareController(controller, "Card");
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_card_wall_view()
        {
            RetrieveWallResponse response = new RetrieveWallResponse();
            response.Cards = new List<CardDTO>();
            response.CardStatuses = new List<CardStatusDTO>();

            using (mockery.Record())
            {
                Expect.Call(cardService.RetrieveWall()).Return(response);
            }

            using (mockery.Playback())
            {
                controller.Index();
            }

            Assert.That(controller.PropertyBag["cards"], Is.EqualTo(response.Cards));
            Assert.That(controller.PropertyBag["statuses"], Is.EqualTo(response.CardStatuses));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\Index"));
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_show_card_view()
        {
            Guid id = Guid.NewGuid();
            CardDetailsDTO cardDetailsDTO = new CardDetailsDTO();
            RetrieveCardResponse response = new RetrieveCardResponse(cardDetailsDTO);

            using (mockery.Record())
            {
                Expect.Call(cardService.RetrieveCard(null)).Return(response)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                controller.Show(id);
            }

            Assert.That(controller.PropertyBag["card"], Is.EqualTo(cardDetailsDTO));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\Show"));
        }

        [Test]
        public void Should_populate_the_property_bag_and_render_the_edit_card_view()
        {
            Guid id = Guid.NewGuid();
            RetrieveCardResponse retrieveCardResponse = new RetrieveCardResponse(new CardDetailsDTO());

            RetrieveCardOptionsResponse cardOptionsResponse = new RetrieveCardOptionsResponse();
            cardOptionsResponse.Owners = new List<UserDTO>();
            cardOptionsResponse.CardTypes = new List<CardTypeDTO>();
            cardOptionsResponse.CardStatuses = new List<CardStatusDTO>();
            cardOptionsResponse.CardPriorities = new List<CardPriorityDTO>();

            using (mockery.Record())
            {
                Expect.Call(cardService.RetrieveCardOptions()).Return(cardOptionsResponse);
                Expect.Call(cardService.RetrieveCard(null)).Return(retrieveCardResponse)
                    .IgnoreArguments();
            }

            using (mockery.Playback())
            {
                controller.Edit(id);
            }

            Assert.That(controller.PropertyBag["card"], Is.EqualTo(retrieveCardResponse.Card));
            Assert.That(controller.PropertyBag["owners"], Is.EqualTo(cardOptionsResponse.Owners));
            Assert.That(controller.PropertyBag["types"], Is.EqualTo(cardOptionsResponse.CardTypes));
            Assert.That(controller.PropertyBag["statuses"], Is.EqualTo(cardOptionsResponse.CardStatuses));
            Assert.That(controller.PropertyBag["priorities"], Is.EqualTo(cardOptionsResponse.CardPriorities));
            Assert.That(controller.SelectedViewName, Is.EqualTo(@"Card\Edit"));
        }

        [Test]
        public void Should_save_the_edited_card_and_redirect_to_the_card_wall()
        {
            CardDetailsDTO cardDetailsDTO = stubery.GetStub<CardDetailsDTO>();
            
            using (mockery.Record())
            {
                Expect.Call(() => cardService.SaveCard(null)).IgnoreArguments();
            }

            using (mockery.Playback())
            {
                controller.Update(cardDetailsDTO);
            }

            Assert.That(Response.RedirectedTo, Is.EqualTo(String.Format(@"/Card/Show.ashx?Id={0}", cardDetailsDTO.Id)));
        }

        [Test]
        public void Should_update_the_card_status_on_an_ajax_call_from_the_client()
        {
            Guid cardId = Guid.NewGuid();
            Guid statusId = Guid.NewGuid();

            Context.CurrentUser = new UserPermissionsDTO(Guid.Empty, null);

            using (mockery.Record())
            {
                Expect.Call(() => cardService.ChangeCardStatus(null)).IgnoreArguments();
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