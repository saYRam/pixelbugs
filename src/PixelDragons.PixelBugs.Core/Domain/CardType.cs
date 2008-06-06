using Castle.ActiveRecord;
using System;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class CardType
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public string Name { get; set; }

        [Property(NotNull = true)]
        public string Colour { get; set; }
    }
}
