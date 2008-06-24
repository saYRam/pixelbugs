using MbUnit.Framework;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy.Tests
{
	[TestFixture]
	public class When_cant_fly
	{
		IFlyBehavior flyBehavior;

		[SetUp]
		public void Setup()
		{
			flyBehavior = new FlyNoWay();
		}

		[Test]
		public void Should_return_flying_message()
		{
			Assert.AreEqual("I'm can't fly", flyBehavior.Fly(), "The wrong flying message was returned");
		}
	}
}
