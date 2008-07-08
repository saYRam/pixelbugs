using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries
{
    public class CardStatusQueries : ICardStatusQueries
    {
        public DetachedCriteria BuildListQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<CardStatus>()
                .AddOrder(new Order("Ordinal", true));

            return criteria;
        }
    }
}