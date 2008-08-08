using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Messages.Impl;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_the_authenticate_response_is_created
    {
        private AuthenticateResponse response;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            response = new AuthenticateResponse(id);
        }

        [Test]
        public void Should_include_the_authenticated_users_id()
        {
            Assert.That(response.Id, Is.EqualTo(id));
        }
    }
}