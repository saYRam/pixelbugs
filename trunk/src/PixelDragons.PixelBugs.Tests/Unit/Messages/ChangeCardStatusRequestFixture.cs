using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_a_change_card_status_request_is_created
    {
        private ChangeCardStatusRequest request;
        private Guid cardId;
        private Guid statusId;

        [SetUp]
        public void SetUp()
        {
            cardId = Guid.NewGuid();
            statusId = Guid.NewGuid();

            request = new ChangeCardStatusRequest(cardId, statusId);
        }

        [Test]
        public void Should_include_the_card_and_status_ids()
        {
            Assert.That(request.CardId, Is.EqualTo(cardId));
            Assert.That(request.StatusId, Is.EqualTo(statusId));
        }

        [Test]
        public void Should_pass_validation_with_correct_data()
        {
            //No exception should be thrown
            request.Validate();
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_with_invalid_card_id_during_validation()
        {
            ChangeCardStatusRequest invalidRequest = new ChangeCardStatusRequest(Guid.Empty, statusId);
            invalidRequest.Validate();
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_with_invalid_status_id_during_validation()
        {
            ChangeCardStatusRequest invalidRequest = new ChangeCardStatusRequest(cardId, Guid.Empty);
            invalidRequest.Validate();
        }
    }
}