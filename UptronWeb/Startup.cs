using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UptronWeb.Startup))]
namespace UptronWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
