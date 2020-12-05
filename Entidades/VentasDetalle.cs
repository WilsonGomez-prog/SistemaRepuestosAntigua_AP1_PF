using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class VentasDetalle
    {
        [Key]
        public int DetalleVentaId { get; set; }
        public int VentaId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un producto.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una cantidad válida de producto.")]
        public float Cantidad { get; set; }

        [Column(TypeName = "Money")]
        public float Total { get; set; }

        public VentasDetalle()
        {
            DetalleVentaId = 0;
            VentaId = 0;
            ProductoId = 0;
            Cantidad = 0.0f;
            Total = 0;
        }

        public VentasDetalle(int ventaId, int productoId, float cantidad, float total)
        {
            VentaId = ventaId;
            ProductoId = productoId;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
