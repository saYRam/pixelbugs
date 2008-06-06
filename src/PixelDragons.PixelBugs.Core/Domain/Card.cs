using Castle.ActiveRecord;
using System;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class Card
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public DateTime CreatedDate { get; set; }

        [Property(NotNull = true)]
        public string Title { get; set; }

        [Property(SqlType = "NVARCHAR(MAX)")]
        public string Description { get; set; }

        [Property(NotNull = true)]
        public float Points { get; set; }

        [BelongsTo(NotNull = true)]
        public User CreatedBy { get; set; }

        [BelongsTo]
        public User Owner { get; set; }

        [Property(SqlType = "int identity(1,1)", NotNull = true, Insert = false, Update = false)]
        public int Number { get; set; }

        [BelongsTo(NotNull = true)]
        public CardType Type { get; set; }

        [BelongsTo(NotNull = true)]
        public CardPriority Priority { get; set; }

        [BelongsTo(NotNull = true)]
        public CardStatus Status { get; set; }
    }
}
