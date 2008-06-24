using MbUnit.Framework;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_mallard_duck_is_in_the_simulator_it
    {
        Duck mallardDuck;

        [SetUp]
        public void Setup()
        {
            mallardDuck = new MallardDuck();
        }

        [Test]
        public void Should_quack_like_a_normal_duck()
        {
            Assert.AreEqual("Quack", mallardDuck.PerformQuack(), "The wrong quacking message was returned");
        }

        [Test]
        public void Should_be_able_to_fly()
        {
            Assert.AreEqual("I'm flying", mallardDuck.PerformFly(), "The wrong flying message was returned");
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.AreEqual("All ducks float, even decoys!", mallardDuck.Swim(), "The wrong swimming message was returned");
        }

        [Test]
        public void Should_look_like_a_mallard_duck()
        {
            Assert.AreEqual("Displaying of a mallard duck is unique", mallardDuck.Display(), "The display message was returned");
        }
    }
}
