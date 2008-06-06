using System;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Domain;

namespace PixelDragons.PixelBugs.Core.Queries
{
    public class UserQueries : IUserQueries
    {
        public DetachedCriteria BuildAuthenticationQuery(string userName, string password)
        {
            DetachedCriteria criteria = DetachedCriteria.For<User>()
                .Add(Expression.Eq("UserName", userName))
                .Add(Expression.Eq("Password", password));

            return criteria;
        }
    }
}
