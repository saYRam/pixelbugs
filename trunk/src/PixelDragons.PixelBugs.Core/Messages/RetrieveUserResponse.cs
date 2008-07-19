using System;
using System.Collections.Generic;
using System.Security.Principal;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class RetrieveUserResponse : IPrincipal, IRetrievedUser
    {
        private readonly Guid id;
        private List<Permission> permissions;
        private string fullName;

        public RetrieveUserResponse(Guid id, List<Permission> permissions, string fullName)
        {
            this.id = id;
            this.permissions = permissions;
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

        public bool HasPermission(Permission permission)
        {
            if(permissions != null)
                return permissions.Contains(permission);

            return false;
        }

        public bool IsInRole(string role)
        {
            //We are not using this method to control authorization, see HasPermission
            return false;
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(FullName, "PixelBugs Authentication"); }
        }
    }
}