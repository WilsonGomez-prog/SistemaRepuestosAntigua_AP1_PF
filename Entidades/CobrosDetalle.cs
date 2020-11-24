using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class CobrosDetalle
    {
        [Key]
        public int CobroDetalleId { get; set; }
        public int CobroId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una venta a crédito por cobrar.")]
        public int VentaCreditoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una fecha válida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe de introducir monto válido.")]
        [Column(TypeName ="Money")]
        public decimal Monto { get; set; }

        public CobrosDetalle()
        {
            CobroDetalleId = 0;
            CobroId = 0;
            VentaCreditoId = 0;
            Fecha = DateTime.Now;
            Monto = 0;
        }

        public CobrosDetalle(int cobroId, int ventaCreditoId, DateTime fecha, decimal monto)
        {
            CobroId = cobroId;
            VentaCreditoId = ventaCreditoId;
            Fecha = fecha;
            Monto = monto;
        }
    }
}
