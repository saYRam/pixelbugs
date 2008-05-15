using System;
using System.Collections.Generic;
using System.Text;

namespace PixelDragons.Commons.Repositories
{
    public class SliceAndCount<T>
    {
        public T[] Slice { get; set; }
        public int TotalCount { get; set; }
    }
}
