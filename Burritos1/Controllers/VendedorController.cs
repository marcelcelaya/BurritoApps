using Burritos1.Models;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
enum MENSAJE
{
    NOMENSAJE,
    EXITO,
    ERROR
}
namespace Burritos1.Controllers
{
    [Authorize(Roles = "Vendedor,Administrador")]
    public class VendedorController : Controller
    {
        BurritoContext db = new BurritoContext();
        // GET: Vendedor
        public ActionResult Index()
        {
            return View();
        }
        #region AgregarProducto
        public ActionResult AgregarProducto()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProducto(Producto producto)
        {
            producto.Vendedor = User.Identity.Name;
            if (ModelState.IsValid)
            {
                BurritoContext db = new BurritoContext();
                db.Productos.Add(producto);
                db.SaveChanges();
                TempData["Message"] = "Agregado";
                
            }
            return RedirectToAction("ListarProductos");
        }
        #endregion

        #region ListarProductos
        public ActionResult ListarProductos()
        {

            if (TempData["Message"] !=null)
            {
                switch (TempData["Message"])
                {
                    case "Modificado": ViewBag.SucessMessage = "Modificado"; break;
                    case "Eliminado": ViewBag.SucessMessage = "Eliminado"; break;
                    case "Agregado": ViewBag.SucessMessage = "Agregado"; break;
                    default: ViewBag.SucessMessage = "Error"; break;
                }

            }
            BurritoContext db = new BurritoContext();
            String vendedor = User.Identity.Name;
            var data = db.Database.SqlQuery<Producto>(
                @"SELECT * FROM dbo.Productoes 
                WHERE Vendedor = @Vendedor", new SqlParameter("@Vendedor", vendedor)).ToList();
            return View(data);
        }

        


        #endregion
        #region ActualizarProductos
        [HttpGet]
        public ActionResult ActualizarProducto(int id)
        {

            Producto producto = db.Productos.Find(id);
            if (producto != null && producto.Vendedor == User.Identity.Name)
            {
                TempData["Message"] = "Exito";
                return View(producto);
            }
            else
            {
                TempData["Message"] = "Error";
                return RedirectToAction("ListarProductos");
            }
        }
        [HttpPost]
        public ActionResult ActualizarProducto(Producto producto)
        {//Hacer View de Satisfacción
            producto.Vendedor = User.Identity.Name;
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Modificado";
            return RedirectToAction("ListarProductos");
        }
        #endregion
        #region EliminarProducto
        [HttpGet]
        public ActionResult EliminarProducto(int id)
        {
            Producto producto = db.Productos.Find(id);
            return View(producto);
        }

        [HttpPost]
        public ActionResult EliminarProducto(Producto producto)
        {

            db.Entry(producto).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["Message"] = "Eliminado";
            return RedirectToAction("ListarProductos");
        }
        #endregion

    }
}