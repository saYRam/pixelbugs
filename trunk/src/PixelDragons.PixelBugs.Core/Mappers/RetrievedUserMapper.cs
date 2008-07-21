using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class RetrievedUserMapper : IRetrievedUserMapper
    {
        public RetrieveUserResponse MapFrom(User user)
        {
            return new RetrieveUserResponse(user.Id, user.FullName);
        }

        public IEnumerable<RetrieveUserResponse> MapCollection(User[] users)
        {
            foreach (User user in users)
            {
                yield return MapFrom(user);
            }
        }
    }
}