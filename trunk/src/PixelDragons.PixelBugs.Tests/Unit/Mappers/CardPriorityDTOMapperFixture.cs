using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Mappers.Impl;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_domain_card_priority_to_a_dto_card_priority
    {
        private ICardPriorityDTOMapper mapper;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            mapper = new CardPriorityDTOMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            CardPriority cardPriority = new CardPriority { Id = id, Name = "High", Colour = "#00ff00" };

            CardPriorityDTO dto = mapper.MapFrom(cardPriority);

            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Name, Is.EqualTo("High"));
            Assert.That(dto.Colour, Is.EqualTo("#00ff00"));
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            CardPriority[] cardPriorities = new[]
                               {
                                   new CardPriority {Id = id, Name = "High", Colour = "#00ff00"},
                                   new CardPriority {Id = id, Name = "High", Colour = "#00ff00"}
                               };

            IEnumerable<CardPriorityDTO> dtos = mapper.MapCollection(cardPriorities);

            foreach (CardPriorityDTO dto in dtos)
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Name, Is.EqualTo("High"));
                Assert.That(dto.Colour, Is.EqualTo("#00ff00"));
            }
        }
    }
}