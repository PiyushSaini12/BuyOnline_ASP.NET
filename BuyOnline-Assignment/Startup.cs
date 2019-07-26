using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuyOnline_Assignment.Startup))]
namespace BuyOnline_Assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
