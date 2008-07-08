using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Queries;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class When_querying_card_status
    {
        [Test]
        public void Should_build_a_valid_list_query()
        {
            ICardStatusQueries queries = new CardStatusQueries();

            DetachedCriteria criteria = queries.BuildListQuery();

            Assert.That(criteria, Is.Not.Null);
        }
    }
}