using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Empleados
    {
        [Key]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un código de empleado válido.")]
        [MaxLength(8, ErrorMessage ="El codigo del empleado no puede exceder los 8 caracteres.")]
        public string Codigo { get; set; }

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        public Empleados()
        {
            EmpleadoId = 0;
            Codigo = "";         
            UsuarioId = 0;
            UsuarioModificador = 0;
        }

        public Empleados(string codigo, int usuarioId)
        {
            Codigo = codigo;
            UsuarioId = usuarioId;
        }
    }
}
