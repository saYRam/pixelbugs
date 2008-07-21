using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public interface IRetrievedUserPermissionsMapper : IMapper<User, RetrieveUserPermissionsResponse>
    {
    }
}