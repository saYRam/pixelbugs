using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardTypeDTO
    {
        private readonly Guid id;
        private readonly string name;
        private readonly string colour;

        public CardTypeDTO(Guid id, string name, string colour)
        {
            this.id = id;
            this.name = name;
            this.colour = colour;
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Colour
        {
            get { return colour; }
        }
    }
}