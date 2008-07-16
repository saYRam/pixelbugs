namespace PixelDragons.Commons.Repositories
{
    /// <summary>
    /// A class to hold the results of a slice query with a total count
    /// </summary>
    /// <typeparam name="T">The entity type that is being queried</typeparam>
    public class SliceAndCount<T>
    {
        /// <summary>
        /// The slice of entity records from the query
        /// </summary>
        public T[] Slice { get; set; }

        /// <summary>
        /// The total number of entities that match the overall query before the slice
        /// </summary>
        public int TotalCount { get; set; }
    }
}