using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_user_to_a_retrieved_user
    {
        private IRetrievedUserMapper mapper;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            mapper = new RetrievedUserMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            User user = new User {Id = id, FirstName = "Andy", LastName = "Pike"};

            RetrieveUserResponse mappedUser = mapper.MapFrom(user);

            Assert.That(mappedUser.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            User[] users = new[]
                               {
                                   new User {Id = id, FirstName = "Andy", LastName = "Pike"},
                                   new User {Id = id, FirstName = "Andy", LastName = "Pike"}
                               };

            IEnumerable<RetrieveUserResponse> mappedUsers = mapper.MapCollection(users);

            foreach (RetrieveUserResponse mappedUser in mappedUsers)
            {
                Assert.That(mappedUser.Id, Is.EqualTo(id));
                Assert.That(mappedUser.FullName, Is.EqualTo("Andy Pike"));
            }
        }
    }
}