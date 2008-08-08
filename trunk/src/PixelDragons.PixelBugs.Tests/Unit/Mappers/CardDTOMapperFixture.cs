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
    public class When_mapping_from_a_domain_card_to_a_dto_card
    {
        private ICardDTOMapper mapper;
        private Guid id;
        private CardStatus cardStatus;
        private CardPriority cardPriority;
        private CardType cardType;
        private Card card;
        
        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid();
            cardStatus = new CardStatus { Id = Guid.NewGuid(), Name = "Open" };
            cardPriority = new CardPriority { Id = Guid.NewGuid(), Name = "High", Colour = "#ff0000" };
            cardType = new CardType { Id = Guid.NewGuid(), Name = "Story", Colour = "#0000ff" };
            card = new Card { Id = id, Title = "A card title", Number = 1, Status = cardStatus, Priority = cardPriority, Type = cardType };

            mapper = new CardDTOMapper();
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            CardDTO dto = mapper.MapFrom(card);

            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Title, Is.EqualTo("A card title"));
            Assert.That(dto.Number, Is.EqualTo(1));
            Assert.That(dto.StatusId, Is.EqualTo(cardStatus.Id));
            Assert.That(dto.TypeColour, Is.EqualTo(cardType.Colour));
            Assert.That(dto.PriorityColour, Is.EqualTo(cardPriority.Colour));
            Assert.That(dto.PriorityName, Is.EqualTo(cardPriority.Name));    
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            Card[] cards = new[] { card, card };

            IEnumerable<CardDTO> dtos = mapper.MapCollection(cards);

            foreach (CardDTO dto in dtos)
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Title, Is.EqualTo("A card title"));
                Assert.That(dto.Number, Is.EqualTo(1));
                Assert.That(dto.StatusId, Is.EqualTo(cardStatus.Id));
                Assert.That(dto.TypeColour, Is.EqualTo(cardType.Colour));
                Assert.That(dto.PriorityColour, Is.EqualTo(cardPriority.Colour));
                Assert.That(dto.PriorityName, Is.EqualTo(cardPriority.Name));    
            }
        }
    }
}