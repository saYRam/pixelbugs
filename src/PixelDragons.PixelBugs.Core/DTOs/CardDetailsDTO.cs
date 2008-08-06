using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public float Points { get; set; }
        public int Number { get; set; }
        public UserDTO Owner { get; set; }
        public CardTypeDTO Type { get; set; }
        public CardPriorityDTO Priority { get; set; }
        public CardStatusDTO Status { get; set; }
    }
}