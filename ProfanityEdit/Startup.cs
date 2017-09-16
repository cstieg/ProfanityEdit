using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProfanityEdit.Startup))]
namespace ProfanityEdit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
