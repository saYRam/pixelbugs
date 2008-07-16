using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries.CardTypes
{
    public class RetrieveCardTypesQuery : IQueryBuilder
    {
        public DetachedCriteria BuildQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<CardType>()
                .AddOrder(new Order("Name", true));

            return criteria;
        }
    }
}