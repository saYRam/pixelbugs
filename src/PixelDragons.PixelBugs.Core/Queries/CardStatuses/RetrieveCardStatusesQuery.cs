using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries.CardStatuses
{
    public class RetrieveCardStatusesQuery : IQueryBuilder
    {
        public DetachedCriteria BuildQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<CardStatus>()
                .AddOrder(new Order("Ordinal", true));

            return criteria;
        }
    }
}