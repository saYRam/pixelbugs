using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
        }

        public UserDTO(Guid id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}