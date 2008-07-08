using NHibernate.Criterion;

namespace PixelDragons.PixelBugs.Core.Queries
{
    public interface ICardStatusQueries
    {
        DetachedCriteria BuildListQuery();
    }
}