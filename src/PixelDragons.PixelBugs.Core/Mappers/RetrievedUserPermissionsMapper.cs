using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Mappers;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class RetrievedUserPermissionsMapper : IRetrievedUserPermissionsMapper
    {
        public RetrieveUserPermissionsResponse MapFrom(User user)
        {
            List<Permission> permissions = new List<Permission>();

            if (user.Roles != null)
            {
                foreach (Role role in user.Roles)
                {
                    permissions.AddRange(role.Permissions);
                }
            }

            return new RetrieveUserPermissionsResponse(user.Id, permissions);
        }

        public IEnumerable<RetrieveUserPermissionsResponse> MapCollection(User[] users)
        {
            foreach (User user in users)
            {
                yield return MapFrom(user);
            }
        }
    }
}