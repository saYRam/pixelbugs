using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardDTO
    {
        public CardDTO()
        {
        }

        public CardDTO(Guid id, string title, int number, Guid statusId, string typeColour, string priorityColour, string priorityName)
        {
            Id = id;
            Title = title;
            Number = number;
            StatusId = statusId;
            TypeColour = typeColour;
            PriorityColour = priorityColour;
            PriorityName = priorityName;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public Guid StatusId { get; set; }
        public string TypeColour { get; set; }
        public string PriorityColour { get; set; }
        public string PriorityName { get; set; }
    }
}