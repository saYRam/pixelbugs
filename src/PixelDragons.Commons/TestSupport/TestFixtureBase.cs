using System.Configuration;

namespace PixelDragons.Commons.TestSupport
{
    public abstract class TestFixtureBase
    {
        protected string server;
        protected int port;
        protected string extension;
        protected string physicalPath;

        protected TestFixtureBase()
        {
            server = ConfigurationManager.AppSettings["server"] ?? "localhost";
            extension = ConfigurationManager.AppSettings["extension"] ?? "rails";
            physicalPath = ConfigurationManager.AppSettings["physicalPath"] ?? "";
            port = 80;
            
            if(ConfigurationManager.AppSettings["port"] != null)
            {
                port = int.Parse(ConfigurationManager.AppSettings["port"]);
            }
        }
    }
}