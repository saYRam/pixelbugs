using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardTypeDTO
    {
        public CardTypeDTO()
        {
        }

        public CardTypeDTO(Guid id, string name, string colour)
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