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
    public class When_mapping_from_a_domain_card_status_to_a_dto_card_status
    {
        private ICardStatusDTOMapper mapper;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            mapper = new CardStatusDTOMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            CardStatus cardStatus = new CardStatus {Id = id, Name="Open"};

            CardStatusDTO dto = mapper.MapFrom(cardStatus);

            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Name, Is.EqualTo("Open"));
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            CardStatus[] cardStatuses = new[]
                               {
                                   new CardStatus {Id = id, Name="Open"},
                                   new CardStatus {Id = id, Name="Open"}
                               };

            IEnumerable<CardStatusDTO> dtos = mapper.MapCollection(cardStatuses);

            foreach (CardStatusDTO dto in dtos)
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Name, Is.EqualTo("Open"));
            }
        }
    }
}