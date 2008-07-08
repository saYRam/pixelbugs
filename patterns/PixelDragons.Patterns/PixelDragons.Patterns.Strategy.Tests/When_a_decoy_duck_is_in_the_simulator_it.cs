using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_decoy_duck_is_in_the_simulator_it
    {
        Duck decoyDuck;

        [SetUp]
        public void Setup()
        {
            decoyDuck = new DecoyDuck();
        }

        [Test]
        public void Should_not_make_a_noise()
        {
            Assert.That(decoyDuck.PerformQuack(), Is.EqualTo("<< Silence >>"));
        }

        [Test]
        public void Should_not_be_able_to_fly()
        {
            Assert.That(decoyDuck.PerformFly(), Is.EqualTo("I can't fly"));
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.That(decoyDuck.Swim(), Is.EqualTo("All ducks float, even decoys!"));
        }

        [Test]
        public void Should_look_like_a_decoy_duck()
        {
            Assert.That(decoyDuck.Display(), Is.EqualTo("Displaying of a decoy duck is unique"));
        }
    }
}
