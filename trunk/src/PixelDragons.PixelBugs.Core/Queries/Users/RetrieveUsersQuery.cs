using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries.Users
{
    public class RetrieveUsersQuery : IQueryBuilder
    {
        public DetachedCriteria BuildQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<User>()
                .AddOrder(new Order("LastName", true));

            return criteria;
        }
    }
}