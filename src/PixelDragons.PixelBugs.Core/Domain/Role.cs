using System;
using System.Collections.Generic;
using Castle.ActiveRecord;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class Role
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public string Name { get; set; }

        [Property(ColumnType = "PixelDragons.PixelBugs.Core.CustomMappings.PermissionsList, PixelDragons.PixelBugs.Core"
            )]
        public List<Permission> Permissions { get; set; }
    }
}