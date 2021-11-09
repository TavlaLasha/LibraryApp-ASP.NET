using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryClientApp.Startup))]
namespace LibraryClientApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
