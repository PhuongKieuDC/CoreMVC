using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUDJqueryAjax.Startup))]
namespace CRUDJqueryAjax
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
