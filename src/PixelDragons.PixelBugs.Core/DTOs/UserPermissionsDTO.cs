using System;
using System.Collections.Generic;
using System.Security.Principal;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.DTOs
{
    public class UserPermissionsDTO : IPrincipalWithPermissions
    {
        private readonly Guid id;
        private List<Permission> permissions;
        
        public UserPermissionsDTO(Guid id, List<Permission> permissions)
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

        public List<Permission> Permissions
        {
            get
            {
                //Guarantee that there is always a permissions list to make it safe for callers
                if(permissions == null)
                    permissions = new List<Permission>();

                return permissions;
            }
        }
    }
}