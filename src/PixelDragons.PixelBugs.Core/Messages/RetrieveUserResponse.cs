using System;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class RetrieveUserResponse
    {
        private readonly Guid id;
        private string fullName;

        public RetrieveUserResponse(Guid id, string fullName)
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