using System.Configuration;

namespace PixelDragons.Commons.TestSupport
{
    public abstract class TestFixtureBase
    {
        protected string Server { get; set; }
        protected string Port { get; set; }
        protected string Extension { get; set; }

        public TestFixtureBase()
        {
            this.Server = ConfigurationManager.AppSettings["server"] ?? "localhost";
            this.Port = ConfigurationManager.AppSettings["port"] ?? "80";
            this.Extension = ConfigurationManager.AppSettings["extension"] ?? "rails";
        }
    }
}
