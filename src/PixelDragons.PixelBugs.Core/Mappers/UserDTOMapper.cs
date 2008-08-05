using System.Collections.Generic;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Core.Mappers;

namespace PixelDragons.PixelBugs.Core.Mappers
{
    public class UserDTOMapper : IUserDTOMapper
    {
        public UserDTO MapFrom(User user)
        {
            if(user != null)
                return new UserDTO(user.Id, user.FullName);

            return null;
        }

        public IEnumerable<UserDTO> MapCollection(User[] users)
        {
            foreach (User user in users)
            {
                yield return MapFrom(user);
            }
        }
    }
}