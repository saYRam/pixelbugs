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
    public class When_mapping_from_a_domain_card_type_to_a_dto_card_type
    {
        private ICardTypeDTOMapper mapper;
        private Guid id;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            mapper = new CardTypeDTOMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            CardType cardType = new CardType {Id = id, Name="Defect", Colour="#ff0000"};

            CardTypeDTO dto = mapper.MapFrom(cardType);

            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Name, Is.EqualTo("Defect"));
            Assert.That(dto.Colour, Is.EqualTo("#ff0000"));
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            CardType[] cardTypes = new[]
                               {
                                   new CardType {Id = id, Name="Defect", Colour="#ff0000"},
                                   new CardType {Id = id, Name="Defect", Colour="#ff0000"}
                               };

            IEnumerable<CardTypeDTO> dtos = mapper.MapCollection(cardTypes);

            foreach (CardTypeDTO dto in dtos)
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Name, Is.EqualTo("Defect"));
                Assert.That(dto.Colour, Is.EqualTo("#ff0000"));
            }
        }
    }
}