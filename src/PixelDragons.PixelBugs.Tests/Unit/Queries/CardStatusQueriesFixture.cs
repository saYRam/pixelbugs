using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Queries;
using NHibernate.Criterion;

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
