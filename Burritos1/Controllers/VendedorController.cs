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
        int Mensaje { get; set; }
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
            }
            return View();
        }
        #endregion
        #region ListarProductos
        public ActionResult ListarProductos()
        {

            if (Mensaje==(int)MENSAJE.EXITO)
            {
                ViewBag.SucessMessage = "Exito";
            }
            if (Mensaje == (int)MENSAJE.ERROR)
            {
                ViewBag.ErrorMessage = "ERROR";
            }
            Mensaje = (int)MENSAJE.NOMENSAJE;

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
                Mensaje = (int)MENSAJE.EXITO;
                return View(producto);
            }
            else
            {
                Mensaje = (int)MENSAJE.ERROR;
                return RedirectToAction("ListarProductos");
            }
        }
        [HttpPost]
        public ActionResult ActualizarProducto(Producto producto)
        {//Hacer View de Satisfacción
            producto.Vendedor = User.Identity.Name;
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            Mensaje = (int)MENSAJE.EXITO;
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
            Mensaje = (int)MENSAJE.EXITO;
            return RedirectToAction("ListarProductos");
        }
        #endregion

    }
}