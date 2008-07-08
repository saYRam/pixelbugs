using System.Configuration;

namespace PixelDragons.Commons.TestSupport
{
    public abstract class TestFixtureBase
    {
        protected string server;
        protected string port;
        protected string extension;

        protected TestFixtureBase()
        {
            server = ConfigurationManager.AppSettings["server"] ?? "localhost";
            port = ConfigurationManager.AppSettings["port"] ?? "80";
            extension = ConfigurationManager.AppSettings["extension"] ?? "rails";
        }
    }
}