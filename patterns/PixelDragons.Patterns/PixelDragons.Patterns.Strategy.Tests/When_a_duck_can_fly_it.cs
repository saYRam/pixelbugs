using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy.Tests
{
	[TestFixture]
	public class When_a_duck_can_fly_it
	{
		IFlyBehavior flyBehavior;

		[SetUp]
		public void Setup()
		{
			flyBehavior = new FlyWithWings();
		}

		[Test]
		public void Should_return_flying_message()
		{
            Assert.That(flyBehavior.Fly(), Is.EqualTo("I'm flying"));
		}
	}
}
