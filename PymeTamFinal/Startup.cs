using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PymeTamFinal.Startup))]
namespace PymeTamFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
