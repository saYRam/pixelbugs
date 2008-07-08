using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries
{
    public class CardPriorityQueries : ICardPriorityQueries
    {
        public DetachedCriteria BuildListQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<CardPriority>()
                .AddOrder(new Order("Ordinal", true));

            return criteria;
        }
    }
}