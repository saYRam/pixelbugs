using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Exceptions;
using PixelDragons.PixelBugs.Core.Messages.SecurityService;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_request_is_created
    {
        private AuthenticateUserRequest request;

        [SetUp]
        public void SetUp()
        {
            request = new AuthenticateUserRequest("userName", "password");
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
        public void Should_throw_an_exception_with_invalid_data_during_validation()
        {
            AuthenticateUserRequest invalidRequest = new AuthenticateUserRequest(null, "");
            invalidRequest.Validate();
        }
    }
}