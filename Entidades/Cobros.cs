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
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir una fecha válida.")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "Money")]
        public float Total { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        [ForeignKey("CobroId")]
        public List<CobrosDetalle> DetalleCobro { get; set; }

        public Cobros()
        {
            CobroId = 0;
            CreditoId = 0;
            EmpleadoId = 0;
            Fecha = DateTime.Now;
            Total = 0;
            UsuarioModificador = 0;
            DetalleCobro = new List<CobrosDetalle>();
        }

        public Cobros(int creditoId, int empleadoId, DateTime fecha, float total)
        {
            CreditoId = creditoId;
            EmpleadoId = empleadoId;
            Fecha = fecha;
            Total = total;
            DetalleCobro = new List<CobrosDetalle>();
        }
    }
}
