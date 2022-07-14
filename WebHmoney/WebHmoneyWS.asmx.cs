using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebHmoney.Models;

namespace WebHmoney
{
    /// <summary>
    /// Descripción breve de WebHmoneyWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WEBHmoneyWS : System.Web.Services.WebService
    {

        ApplicationDbContext context = new ApplicationDbContext();
        //Modelo de Datos
        HmoneyModelContainer db = new HmoneyModelContainer();


        [WebMethod]
        public int AgregarCuenta(string NombreCuenta, string TipoCuenta, string Moneda, decimal BalanceInicial)
        {
            Cuentas cuenta = new Cuentas();

            cuenta.NombreCuenta = NombreCuenta;       
            cuenta.TipoCuenta = TipoCuenta;
            cuenta.Moneda = Moneda;
            cuenta.BalanceInicial = BalanceInicial;
            cuenta.FechaRegistro = DateTime.Now;

            db.Cuentas.Add(cuenta);
            db.SaveChanges();
            return cuenta.Id;

        }

        [WebMethod]
        public List<CuentasWS> ListarCuenta()
        {


            return db.Cuentas.Select(c => new CuentasWS()
            {

                Id = c.Id,
                NombreCuenta = c.NombreCuenta,
                TipoCuenta = c.TipoCuenta,
                Moneda = c.Moneda,
                BalanceInicial = c.BalanceInicial,
                FechaRegistro = c.FechaRegistro,

            }).ToList();

        }

        public class CuentasWS
        {
            public int Id { get; set; }
            public string NombreCuenta { get; set; }
            public string TipoCuenta { get; set; }
            public string Moneda { get; set; }
            public decimal BalanceInicial { get; set; }
            public System.DateTime FechaRegistro { get; set; }
        }


        [WebMethod]
        public int AgregarGasto(string Nombre, string Descripcion, bool EsActivo)
        {
            TipoGasto tipogasto = new TipoGasto();

            tipogasto.Nombre = Nombre;
            tipogasto.Descripcion = Descripcion;
            tipogasto.EsActivo = EsActivo;

            db.TipoGastos.Add(tipogasto);
            db.SaveChanges();
            return tipogasto.Id;
        }

        [WebMethod]
        public List<TipoGastoWS> ListarGasto()
        {


            return db.TipoGastos.Select(g => new TipoGastoWS()
            {

                Id = g.Id,
                Nombre = g.Nombre,
                Descripcion = g.Descripcion,
                EsActivo = g.EsActivo,

            }).ToList();

        }

        public class TipoGastoWS
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public bool EsActivo { get; set; }
        }

        [WebMethod]
        public int AgregarMovimiento(DateTime Fecha, string Descripcion, decimal Monto)
        {
            Movimiento movimiento = new Movimiento();

            movimiento.Descripcion = Descripcion;
            movimiento.Monto = Monto;
            movimiento.Fecha = DateTime.Now;

            db.Movimientos.Add(movimiento);
            db.SaveChanges();
            return movimiento.Id;

        }


        [WebMethod]
        public List<MovimientoWS> ListarMovimiento()
        {

            return db.Movimientos.Select(m => new MovimientoWS()
            {

                Id = m.Id,
                Descripcion = m.Descripcion,
                Monto = m.Monto,
                Fecha = DateTime.Now,

            }).ToList();

        }

        public class MovimientoWS
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public decimal Monto { get; set; }
            public System.DateTime Fecha { get; set; }
        }


        [WebMethod]
        public bool CambiarClave(string correo, string Pass)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var resultado = userManager.Users.Where(u => u.Email.Equals(correo)).FirstOrDefault();
            if (resultado == null)
            {
                return false;
            }
            else
            {
                userManager.RemovePassword(resultado.Id);
                userManager.AddPassword(resultado.Id, Pass);
                return true;
            }

        }


        [WebMethod]
        public ResultadoSW CrearUsuario(string Email, string Pass)
        {
            ResultadoSW resultadoSW = new ResultadoSW();
            ApplicationDbContext context = new ApplicationDbContext();
            //string resultado = "";

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var validarUsuario = UserManager.Users.Where(u => u.Email.Equals(Email)).FirstOrDefault();

            if (validarUsuario == null)
            {

                var user = new ApplicationUser();
                user.UserName = Email;
                user.Email = Email;
                string userPWD = Pass;

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var manejadorRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    if (!manejadorRol.RoleExists("UsuarioAnonimo"))
                    {
                        manejadorRol.Create(new IdentityRole("UsuarioAnonimo"));
                    }

                    UserManager.AddToRole(user.Id, "UsuarioAnonimo");

                    resultadoSW.mensaje = "Se ha creado el usuario satisfactoriamente";
                    resultadoSW.respuesta = true;
                }
                else
                {
                    resultadoSW.mensaje = "No se ha podido crear el usuario";

                    resultadoSW.respuesta = false;
                }

            }
            else
            {
                resultadoSW.mensaje = "El correo ingresado ya existe";
                resultadoSW.respuesta = false;
            }
            return resultadoSW;

        }

        [WebMethod]
        public bool Login(string cuenta, string Pass)
        {
            return Validar(cuenta, Pass);
        }

        private bool Validar(string cuenta, string Pass)
        {

            var result = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>().PasswordSignIn(cuenta, Pass, false, false);

            if (result == SignInStatus.Success)
            {
                return true;
            }
            else
                return false;
        }
        public class ResultadoSW
        {
            public string mensaje;
            public bool respuesta;
        }
    }
}
