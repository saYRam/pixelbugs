﻿using System;
using Castle.ActiveRecord;

namespace PixelDragons.PixelBugs.Core.Domain
{
    [ActiveRecord]
    public class Card
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public string Title { get; set; }

        [Property(SqlType = "NVARCHAR(MAX)")]
        public string Body { get; set; }

        [Property(NotNull = true)]
        public float Points { get; set; }

        [Property(SqlType = "int identity(1,1)", NotNull = true, Insert = false, Update = false)]
        public int Number { get; set; }

        [BelongsTo]
        public User Owner { get; set; }
        
        [BelongsTo(NotNull = true)]
        public CardType Type { get; set; }

        [BelongsTo(NotNull = true)]
        public CardPriority Priority { get; set; }

        [BelongsTo(NotNull = true)]
        public CardStatus Status { get; set; }
    }
}