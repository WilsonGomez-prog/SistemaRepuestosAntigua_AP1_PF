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

        [Required(ErrorMessage = "Debe de elegir una fecha válida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del cliente al que esta cobrando.")]
        public int ClienteId { get; set; }

        [Column(TypeName = "Money")]
        public float Total { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        [ForeignKey("CobroId")]
        public List<CobrosDetalle> DetalleCobro { get; set; }

        public Cobros()
        {
            CobroId = 0;
            Fecha = DateTime.Now;
            Total = 0;
            UsuarioModificador = 0;
            DetalleCobro = new List<CobrosDetalle>();
        }

        public Cobros(DateTime fecha, float total)
        {
            Fecha = fecha;
            Total = total;
            DetalleCobro = new List<CobrosDetalle>();
        }
    }
}
