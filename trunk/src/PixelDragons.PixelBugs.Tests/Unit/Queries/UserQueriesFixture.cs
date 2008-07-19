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
        private MockRepository mockery;

        [SetUp]
        public void SetUp()
        {
            mockery = new MockRepository();
        }

        [Test]
        public void Should_build_a_valid_authentication_query()
        {
            AuthenticateRequest request = mockery.DynamicMock<AuthenticateRequest>();

            DetachedCriteria criteria;
            IQueryBuilder query = new UserAuthenticationQuery(request);
            
            using (mockery.Record())
            {
                Expect.Call(request.UserName).Return("andy.pike");
                Expect.Call(request.Password).Return("password");
            }

            using (mockery.Playback())
            {
                criteria = query.BuildQuery();    
            }
            
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