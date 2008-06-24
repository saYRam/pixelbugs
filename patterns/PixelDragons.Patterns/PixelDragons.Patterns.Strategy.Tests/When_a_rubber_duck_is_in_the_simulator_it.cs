using MbUnit.Framework;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_rubber_duck_is_in_the_simulator_it
    {
        Duck rubberDuck;

        [SetUp]
        public void Setup()
        {
            rubberDuck = new RubberDuck();
        }

        [Test]
        public void Should_squeak_like_a_toy()
        {
            Assert.AreEqual("Squeak", rubberDuck.PerformQuack(), "The wrong quacking message was returned");
        }

        [Test]
        public void Should_not_be_able_to_fly()
        {
            Assert.AreEqual("I can't fly", rubberDuck.PerformFly(), "The wrong flying message was returned");
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.AreEqual("All ducks float, even decoys!", rubberDuck.Swim(), "The wrong swimming message was returned");
        }

        [Test]
        public void Should_look_like_a_rubber_duck()
        {
            Assert.AreEqual("Displaying of a rubber duck is unique", rubberDuck.Display(), "The display message was returned");
        }
    }
}
