using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardPriorityDTO
    {
        public CardPriorityDTO()
        {
        }

        public CardPriorityDTO(Guid id, string name, string colour)
        {
            Id = id;
            Name = name;
            Colour = colour;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
    }
}