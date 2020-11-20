using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un crédito a cobrar.")]
        public int CreditoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un empleado.")]
        public int EmpleadoVentasId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una fecha válida.")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        [ForeignKey("CobroId")]
        public List<CobrosDetalle> DetalleCobro { get; set; }

        public Cobros()
        {
            CobroId = 0;
            CreditoId = 0;
            EmpleadoVentasId = 0;
            Fecha = DateTime.Now;
            Total = 0;
            DetalleCobro = new List<CobrosDetalle>();
        }

        public Cobros(int creditoId, int empleadoVentasId, DateTime fecha, decimal total)
        {
            CreditoId = creditoId;
            EmpleadoVentasId = empleadoVentasId;
            Fecha = fecha;
            Total = total;
            DetalleCobro = new List<CobrosDetalle>();
        }
    }
}
