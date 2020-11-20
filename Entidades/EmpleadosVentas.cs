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

        [Required(ErrorMessage = "Debe de ingresar un nombre válido.")]
        [MaxLength(30, ErrorMessage = "El nombre del empleado no puede exceder los 30 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un código de empleado válido.")]
        [MaxLength(30, ErrorMessage = "El apellido del empleado no puede exceder los 30 caracteres.")]
        public string Apellidos { get; set; }
        public int UsuarioId { get; set; }

        public EmpleadosVentas()
        {
            EmpleadoVentasId = 0;
            Codigo = "";
            Nombres = "";
            Apellidos = "";
            UsuarioId = 0;
        }

        public EmpleadosVentas(string codigo, string nombres, string apellidos, int usuarioId)
        {
            Codigo = codigo;
            Nombres = nombres;
            Apellidos = apellidos;
            UsuarioId = usuarioId;
        }
    }
}
