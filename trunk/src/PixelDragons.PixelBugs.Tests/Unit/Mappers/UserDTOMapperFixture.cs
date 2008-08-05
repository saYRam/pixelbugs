using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_domain_user_to_a_dto_user
    {
        private IUserDTOMapper mapper;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            mapper = new UserDTOMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            User user = new User {Id = id, FirstName = "Andy", LastName = "Pike"};

            UserDTO dto = mapper.MapFrom(user);

            Assert.That(dto.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_map_a_null_user_to_a_null_dto_user()
        {
            UserDTO dto = mapper.MapFrom(null);

            Assert.That(dto, Is.Null);
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            User[] users = new[]
                               {
                                   new User {Id = id, FirstName = "Andy", LastName = "Pike"},
                                   new User {Id = id, FirstName = "Andy", LastName = "Pike"}
                               };

            IEnumerable<UserDTO> dtos = mapper.MapCollection(users);

            foreach (UserDTO dto in dtos)
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.FullName, Is.EqualTo("Andy Pike"));
            }
        }
    }
}