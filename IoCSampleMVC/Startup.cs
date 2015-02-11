using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IoCSampleMVC.Startup))]
namespace IoCSampleMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
