using System.Collections.Generic;

namespace PixelDragons.Commons.Mappers
{
    /// <summary>
    /// Defines the role of a mapper. When implemented, will map from the source to the destination, 
    /// will also map from a list of source instances to a list of destination.
    /// </summary>
    /// <typeparam name="TSource">The source to map from.</typeparam>
    /// <typeparam name="TDestination">The destination to map to.</typeparam>
    public interface IMapper<TSource, TDestination>
    {
        /// <summary>
        /// Performs the required mapping from the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The mapped destination instance.</returns>
        TDestination MapFrom(TSource source);

        /// <summary>
        /// Maps all instance of the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A list of mapped instances.</returns>
        IEnumerable<TDestination> MapCollection(TSource[] source);
    }
}