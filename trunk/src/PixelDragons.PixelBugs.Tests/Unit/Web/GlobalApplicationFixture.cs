using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Web;

namespace PixelDragons.PixelBugs.Tests.Unit.Web
{
    [TestFixture]
    public class When_using_the_web_application
    {
        private GlobalApplication app;

        [SetUp]
        public void SetUp()
        {
            app = new GlobalApplication();
            app.Application_OnStart();
        }

        [Test]
        public void Should_initialize_Windsor_on_start_up()
        {
            Assert.That(app.Container, Is.Not.Null);
            Assert.That(app.Container.Kernel.GraphNodes.Length, Is.EqualTo(1));
        }

        [Test]
        public void Should_dispose_Windsor_on_shut_down()
        {
            app.Application_OnEnd();
            Assert.That(app.Container.Kernel.GraphNodes.Length, Is.EqualTo(0));
        }
    }
}