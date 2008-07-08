using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Queries;
using NHibernate.Criterion;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class When_querying_card_priorities
    {
        [Test]
        public void Should_build_a_valid_list_query()
        {
            ICardPriorityQueries queries = new CardPriorityQueries();

            DetachedCriteria criteria = queries.BuildListQuery();

            Assert.That(criteria, Is.Not.Null);
        }
    }
}
