using MbUnit.Framework;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy.Tests
{
	[TestFixture]
	public class When_a_duck_cant_fly_it
	{
		IFlyBehavior flyBehavior;

		[SetUp]
		public void Setup()
		{
			flyBehavior = new FlyNoWay();
		}

		[Test]
		public void Should_return_cant_fly_message()
		{
			Assert.AreEqual("I can't fly", flyBehavior.Fly(), "The wrong flying message was returned");
		}
	}
}
