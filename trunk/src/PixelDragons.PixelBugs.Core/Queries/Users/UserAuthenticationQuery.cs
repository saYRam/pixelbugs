using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages;

namespace PixelDragons.PixelBugs.Core.Queries.Users
{
    public class UserAuthenticationQuery : IQueryBuilder
    {
        private readonly AuthenticateRequest request;

        public UserAuthenticationQuery(AuthenticateRequest request)
        {
            this.request = request;
        }

        public DetachedCriteria BuildQuery()
        {
            DetachedCriteria criteria = DetachedCriteria.For<User>()
                .Add(Restrictions.Eq("UserName", request.UserName))
                .Add(Restrictions.Eq("Password", request.Password));

            return criteria;
        }
    }
}