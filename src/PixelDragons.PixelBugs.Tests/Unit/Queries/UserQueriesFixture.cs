using System;
using NUnit.Framework;
using NHibernate.Criterion;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class When_querying_users
    {
        [Test]
        public void Should_build_a_valid_authentication_Query()
        { 
            UserQueries queries = new UserQueries();

            DetachedCriteria criteria = queries.BuildAuthenticationQuery("andy.pike", "mypassword");

            Assert.That(criteria, Is.Not.Null);
        }
    }
}
