using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Patterns.Strategy.QuackBehavior;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_duck_can_squeak_it
    {
        IQuackBehavior quackBehavior;

        [SetUp]
        public void Setup()
        {
            quackBehavior = new Squeak();
        }

        [Test]
        public void Should_return_squeak_message()
        {
            Assert.That(quackBehavior.Quack(), Is.EqualTo("Squeak"));
        }
    }
}
