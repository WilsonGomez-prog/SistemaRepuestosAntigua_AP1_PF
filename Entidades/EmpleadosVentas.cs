using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class EmpleadosVentas
    {
        [Key]
        public int EmpleadoVentasId { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un código de empleado válido.")]
        [MaxLength(8, ErrorMessage ="El codigo del empleado no puede exceder los 8 caracteres.")]
        public string Codigo { get; set; }

        public int UsuarioId { get; set; }

        public EmpleadosVentas()
        {
            EmpleadoVentasId = 0;
            Codigo = "";         
            UsuarioId = 0;
        }

        public EmpleadosVentas(string codigo, int usuarioId)
        {
            Codigo = codigo;
            UsuarioId = usuarioId;
        }
    }
}
