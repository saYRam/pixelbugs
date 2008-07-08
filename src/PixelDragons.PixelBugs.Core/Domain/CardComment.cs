using System;
using Castle.ActiveRecord;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class CardComment
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public DateTime CreatedDate { get; set; }

        [BelongsTo(NotNull = true)]
        public Card Card { get; set; }

        [BelongsTo(NotNull = true)]
        public User CreatedBy { get; set; }

        [Property(SqlType = "NVARCHAR(MAX)")]
        public string Text { get; set; }
    }
}