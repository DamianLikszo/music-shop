using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            /*app.UseHangfire(config =>
            {
                config.UseAuthorizationFilters(new AuthorizationFilter
                {
                    Roles = "Admin"
                });

                config.UseSqlServerStorage("StoreContext");
                config.UseServer();
            });*/
        }
    }
}