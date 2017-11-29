using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Burritos1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Burritos1.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {

            List<SelectListItem> ListRoles = new List<SelectListItem>()
            {
              new SelectListItem {Text="Vendedor",Value="Vendedor", },
              new SelectListItem {Text="Administrador",Value="Administrador" }
            };
            ViewBag.ListRoles = ListRoles;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        
        [HttpPost]
        public ActionResult Details(SelectListItem ItemSeleccionado)
        {
            //var ItemSeleccionado.Text;
            string ListRoles = Request.Form["ListRoles"].ToString();
            if (User.Identity.IsAuthenticated)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var idUser = User.Identity.GetUserId();
                    var roleManager = new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(db));
                    //Creando el rol
                    //var resultado = roleManager.Create(new IdentityRole("Usuario"));
                    var userManager = new UserManager<ApplicationUser>(
                        new UserStore<ApplicationUser>(db));
                    //User esta en rol?
                    var userEstaenRol = userManager.IsInRole(idUser, ListRoles);

                    if (!userEstaenRol) {
                        var resultado = userManager.AddToRole(idUser, ListRoles);
                        TempData["Message"] = "UsuarioNuevoRol";
                    }
                    else
                    {
                        TempData["Message"] = "UsuarioRolExistente";
                    }
                    //GetRoles de Usuario 
                    //var roles = userManager.GetRoles(idUser);
                    //Remover Usuario de rol
                    //resultado = userManager.RemoveFromRole(idUser, "Usuario");
                    //borrar Rol
                    //var rolVendedor = roleManager.FindByName("Vendedor");
                    //roleManager.Delete(rolVendedor);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
