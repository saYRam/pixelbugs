using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Messages.Impl;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_retrieve_card_response_is_created
    {
        private RetrieveCardResponse response;
        private CardDetailsDTO cardDetailsDTO;

        [SetUp]
        public void Setup()
        {
            cardDetailsDTO = new CardDetailsDTO();
            response = new RetrieveCardResponse(cardDetailsDTO);
        }

        [Test]
        public void Should_include_the_card_details_dto()
        {
            Assert.That(response.Card, Is.EqualTo(cardDetailsDTO));
        }
    }
}