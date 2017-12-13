using Burritos1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var data = db.Database.SqlQuery<Ordenes>(
                @"SELECT * from dbo.Ordenes WHERE idComprador = @idComprador", new SqlParameter("@idComprador", User.Identity.GetUserId())).ToList();
            Ordenes orden = new Ordenes();
            orden.Cantidad = int.Parse(cantidad);
            orden.idComprador = User.Identity.GetUserId();
            orden.Vendedor = mimodelo.Vendedor;
            orden.Precio = mimodelo.Costo;
            db.Ordenes.Add(orden);
            db.SaveChanges();
          
            
            if (data != null)
            {
                //TempData["Message"] = "Exito";
                return View(data);
            }
            return View();
        }
        public ActionResult MisOrdenes()
        {
            BurritoContext db = new BurritoContext();
            var data = db.Database.SqlQuery<Ordenes>(
                @"SELECT * FROM dbo.Ordenes 
                WHERE idComprador = @idComprador ", new SqlParameter("@idComprador", User.Identity.GetUserId())).ToList();
            if (data != null)
            { 
                return View(data);
            }
            return RedirectToAction("MostrarProducto");
        }
    }
}