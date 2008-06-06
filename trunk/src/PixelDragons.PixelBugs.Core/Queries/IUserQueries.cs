using System;
using NHibernate.Criterion;

namespace PixelDragons.PixelBugs.Core.Queries
{
    public interface IUserQueries
    {
        DetachedCriteria BuildAuthenticationQuery(string userName, string password);
    }
}
