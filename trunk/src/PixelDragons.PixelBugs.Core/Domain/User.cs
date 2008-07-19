using System;
using System.Collections.Generic;
using System.Security.Principal;
using Castle.ActiveRecord;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord("`User`")]
    public class User
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

        [HasAndBelongsToMany(typeof (Role), Table = "UserRole", ColumnKey = "User_Id", ColumnRef = "Role_Id", Lazy = false)]
        public IList<Role> Roles { get; set; }


        //This is not under the control of active record
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
    }
}