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

        [Required(ErrorMessage = "Debe de introducir un nombre de usuario.")]
        [MaxLength(16, ErrorMessage = "El nombre de usuario no puede exceder los 16 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe de introducir una clave de usuario.")]
        [MaxLength(16, ErrorMessage = "La clave de usuario no puede exceder los 16 caracteres.")]
        public string Clave { get; set; }

        public Usuarios()
        {
            UsuarioId = 0;
            NombreUsuario = "";
            Clave = "";
        }

        public Usuarios(string nombreUsuario, string clave)
        {
            NombreUsuario = nombreUsuario;
            Clave = clave;
        }
    }
}
