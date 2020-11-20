using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class VentasContadoDetalle
    {
        [Key]
        public int DetalleVentaContadoId { get; set; }
        public int VentaContadoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un producto.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una cantidad válida de producto.")]
        public float Cantidad { get; set; }

        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        public VentasContadoDetalle()
        {
            DetalleVentaContadoId = 0;
            VentaContadoId = 0;
            ProductoId = 0;
            Cantidad = 0.0f;
            Total = 0;
        }

        public VentasContadoDetalle(int ventaContadoId, int productoId, float cantidad, decimal total)
        {
            VentaContadoId = ventaContadoId;
            ProductoId = productoId;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
