using System;
using NUnit.Framework;
using PixelDragons.PixelBugs.Core.DTOs;
using NUnit.Framework.SyntaxHelpers;

namespace PixelDragons.PixelBugs.Tests.Unit.DTOs
{
    [TestFixture]
    public class When_accessing_a_user_dto
    {
        private UserDTO dto;
        private Guid id;

        [SetUp]
        public void SetUp()
        {
            id = Guid.NewGuid();

            dto = new UserDTO(id, "Andy Pike");
        }

        [Test]
        public void Should_return_the_users_id()
        {
            Assert.That(dto.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_return_the_users_full_name()
        {
            Assert.That(dto.FullName, Is.EqualTo("Andy Pike"));
        }
    }
}