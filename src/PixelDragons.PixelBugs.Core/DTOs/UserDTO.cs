using System;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class UserDTO
    {
        private readonly Guid id;
        private readonly string fullName;

        public UserDTO(Guid id, string fullName)
        {
            this.id = id;
            this.fullName = fullName;
        }

        public Guid Id
        {
            get { return id; }
        }

        public string FullName
        {
            get { return fullName; }
        }
    }
}