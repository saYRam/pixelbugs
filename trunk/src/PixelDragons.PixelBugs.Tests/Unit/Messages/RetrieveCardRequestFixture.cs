using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_retrieve_card_request_is_created
    {
        private RetrieveCardRequest request;
        private Guid id;

        [SetUp]
        public void SetUp()
        {
            id = Guid.NewGuid();
            request = new RetrieveCardRequest(id);
        }

        [Test]
        public void Should_include_the_user_name_and_password()
        {
            Assert.That(request.CardId, Is.EqualTo(id));
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
            RetrieveCardRequest invalidRequest = new RetrieveCardRequest(Guid.Empty);
            invalidRequest.Validate();
        }
    }
}