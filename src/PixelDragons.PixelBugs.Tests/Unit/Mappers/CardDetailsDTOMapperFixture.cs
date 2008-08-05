using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Tests.Unit.Stubs;

namespace PixelDragons.PixelBugs.Tests.Unit.Mappers
{
    [TestFixture]
    public class When_mapping_from_a_domain_card_to_a_card_details_dto
    {
        private ICardDetailsDTOMapper mapper;

        private StubRepository stubery;
        private Card card;

        [SetUp]
        public void Setup()
        {
            stubery = new StubRepository();
            card = stubery.GetStub<Card>();

            mapper = new CardDetailsDTOMapper(new UserDTOMapper(), new CardTypeDTOMapper(), new CardStatusDTOMapper(), new CardPriorityDTOMapper());
        }

        [Test]
        public void Should_map_from_a_single_instance()
        {
            CardDetailsDTO dto = mapper.MapFrom(card);

            AssertCardDetailsDTO(dto);
        }

        [Test]
        public void Should_map_from_a_collection()
        {
            Card[] cards = new Card[] { card, card };
            IEnumerable<CardDetailsDTO> dtos = mapper.MapCollection(cards);

            foreach(CardDetailsDTO dto in dtos)
            {
                AssertCardDetailsDTO(dto);
            }
        }

        private void AssertCardDetailsDTO(CardDetailsDTO dto)
        {
            Assert.That(dto.Id, Is.EqualTo(card.Id));
            Assert.That(dto.CreatedDate, Is.EqualTo(card.CreatedDate));
            Assert.That(dto.CreatedBy.Id, Is.EqualTo(card.CreatedBy.Id));
            Assert.That(dto.CreatedBy.FullName, Is.EqualTo(card.CreatedBy.FullName));
            Assert.That(dto.Title, Is.EqualTo(card.Title));
            Assert.That(dto.Body, Is.EqualTo(card.Body));
            Assert.That(dto.Number, Is.EqualTo(card.Number));
            Assert.That(dto.Points, Is.EqualTo(card.Points));
            Assert.That(dto.Status.Id, Is.EqualTo(card.Status.Id));
            Assert.That(dto.Status.Name, Is.EqualTo(card.Status.Name));
            Assert.That(dto.Priority.Id, Is.EqualTo(card.Priority.Id));
            Assert.That(dto.Priority.Name, Is.EqualTo(card.Priority.Name));
            Assert.That(dto.Type.Id, Is.EqualTo(card.Type.Id));
            Assert.That(dto.Type.Name, Is.EqualTo(card.Type.Name));
            Assert.That(dto.Owner.Id, Is.EqualTo(card.Owner.Id));
            Assert.That(dto.Owner.FullName, Is.EqualTo(card.Owner.FullName));
        }
    }
}