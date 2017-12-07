using System.ComponentModel.DataAnnotations;

namespace Burritos1.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Vendedor { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public float Costo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Disponibles { get; set; }
    }
}