using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Burritos1.Models
{
    public class Ordenes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Orden order { get; set; }
        public string idComprador { get; set; }
    }
    public class Orden
    {
        [Key]
        public int idOrden { get; set; }
        public int idProducto { get; set; }
        public string idVendedor { get; set; }
        public int Cantidad { get; set; }
        public string Producto { get; set; }
        public string Vendedor { get; set; }
        public float Precio { get; set; }
    }

}