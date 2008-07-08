using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
            Assert.That(mallardDuck.PerformQuack(), Is.EqualTo("Quack"));
        }

        [Test]
        public void Should_be_able_to_fly()
        {
            Assert.That(mallardDuck.PerformFly(), Is.EqualTo("I'm flying"));
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.That(mallardDuck.Swim(), Is.EqualTo("All ducks float, even decoys!"));
        }

        [Test]
        public void Should_look_like_a_mallard_duck()
        {
            Assert.That(mallardDuck.Display(), Is.EqualTo("Displaying of a mallard duck is unique"));
        }
    }
}
