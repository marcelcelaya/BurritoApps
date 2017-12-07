using Burritos1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Burritos1.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return RedirectToAction("BurritosenVenta");
        }

        public ActionResult BurritosenVenta()
        {
            BurritoContext db = new BurritoContext();
            return View(db.Productos.ToList());
        }
        [HttpGet]
        public ActionResult MostrarProducto(int id)
        {
            BurritoContext db = new BurritoContext();
            Producto producto = db.Productos.Find(id);
            if (producto != null)
            {
                //TempData["Message"] = "Exito";
                return View(producto);
            }
            return View();
        }

    }
}