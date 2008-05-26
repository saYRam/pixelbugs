using System;
using System.Collections.Generic;
using System.Security.Principal;
using Castle.ActiveRecord;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord("`User`")]
    public class User : IPrincipal
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public string FirstName { get; set; }

        [Property(NotNull = true)]
        public string LastName { get; set; }

        [Property(NotNull = true)]
        public string Email { get; set; }

        [Property(NotNull = true)]
        public string UserName { get; set; }

        [Property(NotNull = true)]
        public string Password { get; set; }
        
        [HasAndBelongsToMany(typeof(Role), Table = "UserRole", ColumnKey = "User_Id", ColumnRef = "Role_Id", Lazy = false)]
        public IList<Role> Roles { get; set; }


        //This is not under the control of active record
        public string FullName 
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public bool HasPermission(string permissionAsText)
        {
            try
            {
                Permission permission = (Permission)Enum.Parse(typeof(Permission), permissionAsText, true);

                return HasPermission(permission);
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException( String.Format("Unable to convert '{0}' to a system permission.", permissionAsText), ex));
            }
        }

        public bool HasPermission(Permission permission)
        {
            if (this.Roles != null)
            {
                foreach (Role role in this.Roles)
                {
                    if (role.Permissions != null && role.Permissions.Contains(permission))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #region IPrincipal Members
        public IIdentity Identity
        {
            get { return new GenericIdentity(this.FullName, "PixelBugs Authentication"); }
        }

        public bool IsInRole(string role)
        {
            //We are not using this method to control authorization, see HasPermission
            return false;
        }
        #endregion
    }
}
