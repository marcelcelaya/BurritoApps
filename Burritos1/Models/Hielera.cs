using System.ComponentModel.DataAnnotations;

namespace Burritos1.Models
{
    public class Hielera
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string idVendedor { get; set; }
        [Required]
        public string idProducto { get; set; }
        [Required]
        public int Disponibles { get; set; }

    }
}