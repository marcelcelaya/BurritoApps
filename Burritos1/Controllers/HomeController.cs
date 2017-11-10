using Burritos1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Burritos1.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var estaAutenticado = User.Identity.IsAuthenticated;
            if (estaAutenticado)
            {
                var NombreUsuario = User.Identity.Name;
                var id = User.Identity.GetUserId();
                //Toda la informacion del usuario
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var usuario = db.Users.Where(x => x.Id == id).FirstOrDefault();
                    var emailConfirmado = usuario.EmailConfirmed;

                    /*var userManager = new UserManager<ApplicationUser>(
                        new UserStore<ApplicationUser>(db));
                    var user = new ApplicationUser();
                    user.UserName = "Marcel";
                    user.Email = "Marcelcelaya@gmail.com";
                    //Crear Usuario 
                    var resultado = userManager.Create(user, "MiPasswordSecreto");*/
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}