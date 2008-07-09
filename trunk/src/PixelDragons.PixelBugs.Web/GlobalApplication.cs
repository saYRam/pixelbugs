using System.Web;
using Castle.ActiveRecord;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace PixelDragons.PixelBugs.Web
{
    public class GlobalApplication : HttpApplication, IContainerAccessor
    {
        private static IWindsorContainer container;

        public void Application_OnStart()
        {
            container = new WindsorContainer(new XmlInterpreter());

            //ActiveRecordStarter.CreateSchema();
        }

        public void Application_OnEnd()
        {
            container.Dispose();
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }
    }
}