using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardStatusDTO
    {
        private readonly Guid id;
        private readonly string name;

        public CardStatusDTO(Guid id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}