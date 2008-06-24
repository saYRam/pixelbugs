using MbUnit.Framework;

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
            Assert.AreEqual("<< Silence >>", decoyDuck.PerformQuack(), "The wrong quacking message was returned");
        }

        [Test]
        public void Should_not_be_able_to_fly()
        {
            Assert.AreEqual("I can't fly", decoyDuck.PerformFly(), "The wrong flying message was returned");
        }

        [Test]
        public void Should_be_able_to_swim()
        {
            Assert.AreEqual("All ducks float, even decoys!", decoyDuck.Swim(), "The wrong swimming message was returned");
        }

        [Test]
        public void Should_look_like_a_decoy_duck()
        {
            Assert.AreEqual("Displaying of a decoy duck is unique", decoyDuck.Display(), "The display message was returned");
        }
    }
}
