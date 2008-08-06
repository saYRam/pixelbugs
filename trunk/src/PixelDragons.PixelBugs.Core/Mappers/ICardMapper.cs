using PixelDragons.Commons.Mappers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public interface ICardMapper : IMapper<CardDetailsDTO, Card>
    {
    }
}