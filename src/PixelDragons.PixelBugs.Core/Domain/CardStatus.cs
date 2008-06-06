using Castle.ActiveRecord;
using System;
using System.Drawing;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class CardStatus
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public string Name { get; set; }

        [Property(NotNull = true)]
        public int Ordinal { get; set; }
    }
}
