using MbUnit.Framework;
using PixelDragons.Patterns.Strategy.QuackBehavior;

namespace PixelDragons.Patterns.Strategy.Tests
{
    [TestFixture]
    public class When_a_duck_can_quack_it
    {
        IQuackBehavior quackBehavior;

        [SetUp]
        public void Setup()
        {
            quackBehavior = new NormalQuack();
        }

        [Test]
        public void Should_return_quack_message()
        {
            Assert.AreEqual("Quack", quackBehavior.Quack(), "The wrong quacking message was returned");
        }
    }
}
