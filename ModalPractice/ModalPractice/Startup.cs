using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModalPractice.Startup))]
namespace ModalPractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
