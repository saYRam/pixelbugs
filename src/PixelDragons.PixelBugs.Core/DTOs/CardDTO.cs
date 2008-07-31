using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class CardDTO
    {
        private readonly Guid id;
        private string title;
        private int number;
        private Guid statusId;
        private string typeColour;
        private string priorityColour;
        private string priorityName;

        public CardDTO(Guid id, string title, int number, Guid statusId, string typeColour, string priorityColour, string priorityName)
        {
            this.id = id;
            this.title = title;
            this.number = number;
            this.statusId = statusId;
            this.typeColour = typeColour;
            this.priorityColour = priorityColour;
            this.priorityName = priorityName;
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Title
        {
            get { return title; }
        }

        public int Number
        {
            get { return number; }
        }

        public Guid StatusId
        {
            get { return statusId; }
        }

        public string TypeColour
        {
            get { return typeColour; }
        }

        public string PriorityColour
        {
            get { return priorityColour; }
        }

        public string PriorityName
        {
            get { return priorityName; }
        }
    }
}