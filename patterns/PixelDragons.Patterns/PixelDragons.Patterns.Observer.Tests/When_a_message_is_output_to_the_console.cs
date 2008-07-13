using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace PixelDragons.Patterns.Observer.Tests
{
    [TestFixture]
    public class When_a_message_is_output_to_the_console
    {
        [Test]
        public void Should_cache_the_last_message()
        {
            IConsole console = new DisplayConsole();

            const string message = "This is a message to output to the console";

            console.WriteLine(message);

            Assert.That(console.LastOutput, Is.EqualTo(message));
        }
    }
}