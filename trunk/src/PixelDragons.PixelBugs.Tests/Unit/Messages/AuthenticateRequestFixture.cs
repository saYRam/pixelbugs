using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Tests.Unit.Stubs;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_authenticate_request_is_created
    {
        private AuthenticateRequest request;
        private StubRepository stubery;

        [SetUp]
        public void SetUp()
        {
            stubery = new StubRepository();
            request = stubery.GetStub<AuthenticateRequest>();
        }

        [Test]
        public void Should_include_the_user_name_and_password()
        {
            Assert.That(request.UserName, Is.EqualTo("userName"));
            Assert.That(request.Password, Is.EqualTo("password"));
        }

        [Test]
        public void Should_pass_validation_with_correct_data()
        {
            //No exception should be thrown
            request.Validate();
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_with_invalid_user_name_during_validation()
        {
            AuthenticateRequest invalidRequest = stubery.GetStub<AuthenticateRequest>();
            invalidRequest.UserName = null;
            invalidRequest.Validate();
        }

        [Test]
        [ExpectedException(typeof(InvalidRequestException))]
        public void Should_throw_an_exception_with_invalid_password_during_validation()
        {
            AuthenticateRequest invalidRequest = stubery.GetStub<AuthenticateRequest>();
            invalidRequest.Password = null;
            invalidRequest.Validate();
        }
    }
}