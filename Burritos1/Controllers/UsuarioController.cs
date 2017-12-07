using Burritos1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
        public ActionResult MostrarProducto(Producto mimodelo,string cantidad)
        {
            BurritoContext db = new BurritoContext();
            Producto producto = db.Productos.Find(mimodelo.Id);
            Orden MiOrden = new Orden();
            MiOrden.Cantidad = int.Parse(cantidad);
            MiOrden.idComprador = User.Identity.GetUserId();
            MiOrden.idVendedor = producto.Vendedor;
            MiOrden.idProducto = producto.Id;
            Ordenes SuperOrden = new Ordenes();
            SuperOrden.order = MiOrden;
            db.Ordenes.Add(SuperOrden);
            db.SaveChanges();
            /*
            if (producto != null)
            {
                //TempData["Message"] = "Exito";
                return View(producto);
            }*/
            return View();
        }
    }
}