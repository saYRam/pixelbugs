using Castle.ActiveRecord;
using System;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class Issue
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public DateTime CreatedDate { get; set; }

        [Property(NotNull = true)]
        public string Summary { get; set; }

        [Property(SqlType = "NVARCHAR(MAX)")]
        public string Description { get; set; }

        [BelongsTo]
        public User ReportedBy { get; set; }

        [BelongsTo]
        public User AssignedTo { get; set; }
    }
}
