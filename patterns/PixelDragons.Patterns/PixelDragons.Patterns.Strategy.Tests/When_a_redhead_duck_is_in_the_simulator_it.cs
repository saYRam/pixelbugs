using MbUnit.Framework;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_redhead_duck_is_in_the_simulator_it
    {
        Duck redheadDuck;

        [SetUp]
        public void Setup()
        {
            redheadDuck = new RedheadDuck();
        }

        [Test]
        public void Should_quack_like_a_normal_duck()
        {
            Assert.AreEqual("Quack", redheadDuck.PerformQuack(), "The wrong quacking message was returned");
        }

        [Test]
        public void Should_be_able_to_fly()
        {
            Assert.AreEqual("I'm flying", redheadDuck.PerformFly(), "The wrong flying message was returned");
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.AreEqual("All ducks float, even decoys!", redheadDuck.Swim(), "The wrong swimming message was returned");
        }

        [Test]
        public void Should_look_like_a_redhead_duck()
        {
            Assert.AreEqual("Displaying of a redhead duck is unique", redheadDuck.Display(), "The display message was returned");
        }
    }
}
