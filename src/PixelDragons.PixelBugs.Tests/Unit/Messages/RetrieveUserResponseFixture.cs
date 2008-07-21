using System;
using NUnit.Framework;
using PixelDragons.PixelBugs.Core.Messages;
using NUnit.Framework.SyntaxHelpers;

namespace PixelDragons.PixelBugs.Tests.Unit.Messages
{
    [TestFixture]
    public class When_accessing_a_retrieved_user
    {
        private RetrieveUserResponse response;
        private Guid id;

        [SetUp]
        public void SetUp()
        {
            id = Guid.NewGuid();

            response = new RetrieveUserResponse(id, "Andy Pike");
        }

        [Test]
        public void Should_return_the_users_id()
        {
            Assert.That(response.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_return_the_users_full_name()
        {
            Assert.That(response.FullName, Is.EqualTo("Andy Pike"));
        }
    }
}