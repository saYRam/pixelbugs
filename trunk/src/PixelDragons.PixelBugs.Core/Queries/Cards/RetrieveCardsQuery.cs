using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries.Cards
{
    public class RetrieveCardsQuery : IQueryBuilder
    {
        public DetachedCriteria BuildQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Card>()
                .CreateAlias("Priority", "priority")
                .AddOrder(new Order("priority.Ordinal", true));

            return criteria;
        }
    }
}