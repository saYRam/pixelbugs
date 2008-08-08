using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class RetrieveWallResponse
    {
        public IEnumerable<CardDTO> Cards { get; set; }
        public IEnumerable<CardStatusDTO> CardStatuses { get; set; }
    }
}