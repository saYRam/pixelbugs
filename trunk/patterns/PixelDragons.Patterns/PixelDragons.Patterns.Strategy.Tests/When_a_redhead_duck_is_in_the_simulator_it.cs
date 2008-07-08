using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
            Assert.That(redheadDuck.PerformQuack(), Is.EqualTo("Quack"));
        }

        [Test]
        public void Should_be_able_to_fly()
        {
            Assert.That(redheadDuck.PerformFly(), Is.EqualTo("I'm flying"));
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.That(redheadDuck.Swim(), Is.EqualTo("All ducks float, even decoys!"));
        }

        [Test]
        public void Should_look_like_a_redhead_duck()
        {
            Assert.That(redheadDuck.Display(), Is.EqualTo("Displaying of a redhead duck is unique"));
        }
    }
}
