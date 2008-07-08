using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
            Assert.That(rubberDuck.PerformQuack(), Is.EqualTo("Squeak"));
        }

        [Test]
        public void Should_not_be_able_to_fly()
        {
            Assert.That(rubberDuck.PerformFly(), Is.EqualTo("I can't fly"));
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.That(rubberDuck.Swim(), Is.EqualTo("All ducks float, even decoys!"));
        }

        [Test]
        public void Should_look_like_a_rubber_duck()
        {
            Assert.That(rubberDuck.Display(), Is.EqualTo("Displaying of a rubber duck is unique"));
        }
    }
}
