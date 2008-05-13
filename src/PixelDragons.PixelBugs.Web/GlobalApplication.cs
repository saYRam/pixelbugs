using System.Web;

using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace PixelDragons.PixelBugs.Web
{
    public class GlobalApplication : HttpApplication, IContainerAccessor
    {
        private static IWindsorContainer _container;

        public void Application_OnStart()
        {
            _container = new WindsorContainer(new XmlInterpreter());
        }

        public void Application_OnEnd()
        {
            _container.Dispose();
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }
    }
}
