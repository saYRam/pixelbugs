using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class RetrieveCardOptionsResponse
    {
        public IEnumerable<UserDTO> Owners { get; set; }
        public IEnumerable<CardTypeDTO> CardTypes { get; set; }
        public IEnumerable<CardStatusDTO> CardStatuses { get; set; }
        public IEnumerable<CardPriorityDTO> CardPriorities { get; set; }
    }
}