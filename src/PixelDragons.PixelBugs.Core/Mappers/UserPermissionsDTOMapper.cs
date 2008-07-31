using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class UserPermissionsDTOMapper : IUserPermissionsDTOMapper
    {
        public UserPermissionsDTO MapFrom(User user)
        {
            List<Permission> permissions = new List<Permission>();

            if (user.Roles != null)
            {
                foreach (Role role in user.Roles)
                {
                    permissions.AddRange(role.Permissions);
                }
            }

            return new UserPermissionsDTO(user.Id, permissions);
        }

        public IEnumerable<UserPermissionsDTO> MapCollection(User[] users)
        {
            foreach (User user in users)
            {
                yield return MapFrom(user);
            }
        }
    }
}