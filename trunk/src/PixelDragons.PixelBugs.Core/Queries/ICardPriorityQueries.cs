using NHibernate.Criterion;

namespace PixelDragons.PixelBugs.Core.Queries
{
    public interface ICardPriorityQueries
    {
        DetachedCriteria BuildListQuery();
    }
}
