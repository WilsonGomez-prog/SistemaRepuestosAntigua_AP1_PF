using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un nombre válido.")]
        [MaxLength(30, ErrorMessage = "El nombre del empleado no puede exceder los 30 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un código de empleado válido.")]
        [MaxLength(30, ErrorMessage = "El apellido del empleado no puede exceder los 30 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe de elegir una fecha válida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe de introducir un nombre de usuario.")]
        [MaxLength(16, ErrorMessage = "El nombre de usuario no puede exceder los 16 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe de introducir una clave de usuario.")]
        [MaxLength(32, ErrorMessage = "La clave de usuario no puede exceder los 32 caracteres.")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        [Required(ErrorMessage = "Debe de indicar el tipo de permisos del usuario.")]
        public int EsAdmin { get; set; }

        public Usuarios()
        {
            UsuarioId = 0;
            NombreUsuario = "";
            Fecha = DateTime.Now;
            Nombres = "";
            Apellidos = "";
            Clave = "";
            UsuarioModificador = 0;
        }

        public Usuarios(DateTime fecha, string nombres, string apellidos, string nombreUsuario, string clave)
        {
            Fecha = fecha;
            Nombres = nombres;
            Apellidos = apellidos;
            NombreUsuario = nombreUsuario;
            Clave = clave;
        }
    }
}
