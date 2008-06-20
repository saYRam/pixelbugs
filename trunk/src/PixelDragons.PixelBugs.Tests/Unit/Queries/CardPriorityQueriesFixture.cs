using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using PixelDragons.PixelBugs.Core.Queries;
using NHibernate.Criterion;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class CardPriorityQueriesFixture
    {
        [Test]
        public void BuildListQuery_Success()
        {
            ICardPriorityQueries queries = new CardPriorityQueries();

            DetachedCriteria criteria = queries.BuildListQuery();

            Assert.IsNotNull(criteria);
        }
    }
}
