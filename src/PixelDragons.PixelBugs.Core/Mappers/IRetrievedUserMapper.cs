using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public interface IRetrievedUserMapper : IMapper<User, RetrieveUserResponse>
    {
    }
}