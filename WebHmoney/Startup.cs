using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebHmoney.Models;

[assembly: OwinStartupAttribute(typeof(WebHmoney.Startup))]
namespace WebHmoney
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void creacionRoles()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var manejadorRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!manejadorRol.RoleExists("Administrador"))
                {
                    // IdentityResult resultado = manejadorRol.Create(new IdentityRole("Administrador"));
                    manejadorRol.Create(new IdentityRole("Administrador"));

                    var manejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var user = new ApplicationUser();
                    user.UserName = "admin@gmail.com";
                    user.Email = "admin@gmail.com";
                    string pass = "admin123";

                    var t = manejadorUsuario.Create(user, pass);
                    if (t.Succeeded)
                        manejadorUsuario.AddToRole(user.Id, "Administrador");

                }
                if (!manejadorRol.RoleExists("Cliente"))
                {
                    // IdentityResult resultado = manejadorRol.Create(new IdentityRole("Administrador"));
                    manejadorRol.Create(new IdentityRole("Cliente"));

                    var manejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var user = new ApplicationUser();
                    user.UserName = "cliente@gmail.com";
                    user.Email = "cliente@gmail.com";
                    string pass = "cliente123";
                    var t = manejadorUsuario.Create(user, pass);

                    if (t.Succeeded)
                        manejadorUsuario.AddToRole(user.Id, "Cliente");

                }

            }

        }
    }
}
