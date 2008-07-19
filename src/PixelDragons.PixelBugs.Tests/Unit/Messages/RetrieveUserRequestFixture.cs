using System;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;
using NUnit.Framework;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_retrieve_user_request_is_created
    {
        private RetrieveUserRequest request;
        private Guid id;

        [SetUp]
        public void SetUp()
        {
            id = Guid.NewGuid();
            request = new RetrieveUserRequest(id);
        }

        [Test]
        public void Should_include_the_id()
        {
            Assert.That(request.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_pass_validation_with_correct_data()
        {
            //No exception should be thrown
            request.Validate();
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_with_invalid_data_during_validation()
        {
            RetrieveUserRequest invalidRequest = new RetrieveUserRequest(Guid.Empty);
            invalidRequest.Validate();
        }
    }   
}