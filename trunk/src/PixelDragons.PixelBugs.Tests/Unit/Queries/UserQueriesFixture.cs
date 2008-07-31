using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Messages;
using PixelDragons.PixelBugs.Core.Queries.Users;
using Rhino.Mocks;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class When_querying_users
    {
        [Test]
        public void Should_build_a_valid_authentication_query()
        {
            AuthenticateRequest request = new AuthenticateRequest("andy.pike", "password");

            IQueryBuilder query = new UserAuthenticationQuery(request);
            
            DetachedCriteria criteria = query.BuildQuery();    
            
            Assert.That(criteria, Is.Not.Null);
        }

        [Test]
        public void Should_build_a_valid_list_query()
        {
            IQueryBuilder query = new RetrieveUsersQuery();

            DetachedCriteria criteria = query.BuildQuery();

            Assert.That(criteria, Is.Not.Null);
        }
    }
}