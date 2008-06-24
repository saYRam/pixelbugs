﻿using MbUnit.Framework;
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
			Assert.AreEqual("I'm flying", flyBehavior.Fly(), "The wrong flying message was returned");
		}
	}
}