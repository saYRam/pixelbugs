using NHibernate.Criterion;

namespace PixelDragons.Commons.Repositories
{
    /// <summary>
    /// A query builder for a particular query which is then used by a repository to query it's data
    /// </summary>
    public interface IQueryBuilder    
    {
        /// <summary>
        /// Builds a <see cref="DetachedCriteria"/> from the available data for this query
        /// </summary>
        /// <returns>Returns a valid <see cref="DetachedCriteria"/></returns>
        DetachedCriteria BuildQuery();
    }
}