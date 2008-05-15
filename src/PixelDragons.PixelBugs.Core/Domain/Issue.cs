using Castle.ActiveRecord;
using System;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class Issue
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property]
        public string Summary { get; set; }

        [Property]
        public string Description { get; set; }
    }
}
