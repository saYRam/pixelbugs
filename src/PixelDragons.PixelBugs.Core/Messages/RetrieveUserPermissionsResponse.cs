using System;
using System.Collections.Generic;
using System.Security.Principal;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class RetrieveUserPermissionsResponse : IPrincipalWithPermissions
    {
        private Guid id;
        private List<Permission> permissions;

        public RetrieveUserPermissionsResponse(Guid id, List<Permission> permissions)
        {
            this.id = id;
            this.permissions = permissions;
        }

        public bool IsInRole(string role)
        {
            //We are not using this method to control authorization, see HasPermission
            return false;
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Id.ToString(), "PixelBugs Authentication"); }
        }

        public Guid Id
        {
            get { return id; }
        }

        public bool HasPermission(Permission permission)
        {
            if (permissions != null)
                return permissions.Contains(permission);

            return false;
        }
    }
}