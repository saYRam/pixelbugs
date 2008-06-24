using MbUnit.Framework;
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
            Assert.AreEqual("Squeak", quackBehavior.Quack(), "The wrong quacking message was returned");
        }
    }
}
