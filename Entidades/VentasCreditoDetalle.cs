using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class VentasCreditoDetalle
    {
        [Key]
        public int DetalleVentaCreditoId { get; set; }
        public int VentaCreditoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un producto.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una cantidad válida de producto.")]
        public float Cantidad { get; set; }

        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        public VentasCreditoDetalle()
        {
            DetalleVentaCreditoId = 0;
            VentaCreditoId = 0;
            ProductoId = 0;
            Cantidad = 0.0f;
            Total = 0;
        }

        public VentasCreditoDetalle(int ventaCreditoId, int productoId, float cantidad, decimal total)
        {
            VentaCreditoId = ventaCreditoId;
            ProductoId = productoId;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
