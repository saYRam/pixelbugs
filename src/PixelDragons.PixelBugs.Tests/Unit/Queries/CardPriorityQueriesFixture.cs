﻿using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Commons.Repositories;
using PixelDragons.PixelBugs.Core.Queries.CardPriorities;

namespace PixelDragons.PixelBugs.Tests.Unit.Queries
{
    [TestFixture]
    public class When_querying_card_priorities
    {
        [Test]
        public void Should_build_a_valid_list_query()
        {
            IQueryBuilder query = new RetrieveCardPrioritiesQuery();

            DetachedCriteria criteria = query.BuildQuery();

            Assert.That(criteria, Is.Not.Null);
        }
    }
}