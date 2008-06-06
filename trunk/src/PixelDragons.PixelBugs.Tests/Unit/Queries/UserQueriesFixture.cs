using System;
using MbUnit.Framework;
using NHibernate.Criterion;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class UserQueriesFixture
    {
        [Test]
        public void BuildAuthenticationQuery_Success()
        { 
            UserQueries queries = new UserQueries();

            DetachedCriteria criteria = queries.BuildAuthenticationQuery("andy.pike", "mypassword");

            Assert.IsNotNull(criteria);
        }
    }
}
