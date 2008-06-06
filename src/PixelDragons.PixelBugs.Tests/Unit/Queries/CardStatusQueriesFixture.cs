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
    public class CardStatusQueriesFixture
    {
        [Test]
        public void BuildAuthenticationQuery_Success()
        {
            ICardStatusQueries queries = new CardStatusQueries();

            DetachedCriteria criteria = queries.BuildListQuery();

            Assert.IsNotNull(criteria);
        }
    }
}
