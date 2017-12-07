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
        public LinkedList<Orden> vectorOrdenes { get; set; }
    }
    public class Orden
    {
        public int idProducto { get; set; }
        public int idComprador { get; set; }
        public int idVendedor { get; set; }
        public int Cantidad { get; set; }
    }
}