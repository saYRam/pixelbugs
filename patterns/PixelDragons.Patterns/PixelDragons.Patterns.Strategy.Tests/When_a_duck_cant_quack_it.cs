using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.Patterns.Strategy.QuackBehavior;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_duck_cant_quack_it
    {
        IQuackBehavior quackBehavior;

        [SetUp]
        public void Setup()
        {
            quackBehavior = new MuteQuack();
        }

        [Test]
        public void Should_return_cant_quack_message()
        {
            Assert.That(quackBehavior.Quack(), Is.EqualTo("<< Silence >>"));
        }
    }
}
