using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardStatusDTO
    {
        public CardStatusDTO()
        {
        }

        public CardStatusDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}