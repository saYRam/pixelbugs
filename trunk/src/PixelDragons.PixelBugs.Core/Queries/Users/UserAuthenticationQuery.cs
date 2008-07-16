using NHibernate.Criterion;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.Messages.SecurityService;

namespace PixelDragons.PixelBugs.Core.Queries.Users
{
    public class UserAuthenticationQuery : IQueryBuilder
    {
        private readonly AuthenticateUserRequest request;

        public UserAuthenticationQuery(AuthenticateUserRequest request)
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