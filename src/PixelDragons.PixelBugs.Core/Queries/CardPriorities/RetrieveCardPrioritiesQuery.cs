using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries.CardPriorities
{
    public class RetrieveCardPrioritiesQuery : IQueryBuilder
    {
        public DetachedCriteria BuildQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<CardPriority>()
                .AddOrder(new Order("Ordinal", true));

            return criteria;
        }
    }
}