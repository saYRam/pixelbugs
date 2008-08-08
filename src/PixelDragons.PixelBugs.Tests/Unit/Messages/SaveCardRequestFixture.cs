using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages.Impl;
using PixelDragons.PixelBugs.Tests.Unit.Stubs;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_save_card_request_is_created
    {
        private SaveCardRequest request;
        private StubRepository stubery;
        private CardDetailsDTO cardDetailsDTO;

        [SetUp]
        public void SetUp()
        {
            stubery = new StubRepository();

            cardDetailsDTO = stubery.GetStub<CardDetailsDTO>();
            request = new SaveCardRequest(cardDetailsDTO);
        }

        [Test]
        public void Should_include_the_card_details_dto()
        {
            Assert.That(request.CardDetailsDTO, Is.EqualTo(cardDetailsDTO));
        }

        [Test]
        public void Should_pass_validation_with_correct_data()
        {
            //No exception should be thrown
            request.Validate();
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_with_null_dto_passed()
        {
            SaveCardRequest invalidRequest = new SaveCardRequest(null);
            invalidRequest.Validate();
        }
    }
}