using PixelDragons.PixelBugs.Core.DTOs;

namespace PixelDragons.PixelBugs.Core.Messages.Impl
{
    public class RetrieveUserPermissionsResponse
    {
        private readonly UserPermissionsDTO user;

        public RetrieveUserPermissionsResponse(UserPermissionsDTO user)
        {
            this.user = user;
        }

        public UserPermissionsDTO User
        {
            get { return user; }
        }
    }
}