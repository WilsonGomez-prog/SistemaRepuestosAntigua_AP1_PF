using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe de introducir un número de cédula válido.")]
        [MaxLength(13, ErrorMessage = "El número de cédula no puede exceder los 13 caracteres.")]
        public string NoCedula { get; set; }

        [Required(ErrorMessage = "Debe de introducir un RNC válido.")]
        [MaxLength(13, ErrorMessage = "El RNC no puede exceder los 13 caracteres.")]
        public string Rnc { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un nombre válido.")]
        [MaxLength(30, ErrorMessage = "El nombre del cliente no puede exceder los 30 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un apellido válido.")]
        [MaxLength(30, ErrorMessage = "El apellido del cliente no puede exceder los 30 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe de ingresar una direccion valida.")]
        [MaxLength(60, ErrorMessage = "La direccion del cliente no puede exceder los 60 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un número telefónico válido.")]
        [MaxLength(16, ErrorMessage = "El número telefónico no puede exceder los 16 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        public Clientes()
        {
            ClienteId = 0;
            NoCedula = "";
            Rnc = "";
            Nombres = "";
            Apellidos = "";
            Telefono = "";
            UsuarioModificador = 0;
        }

        public Clientes(string noCedula, string rnc, string nombres, string apellidos, string telefono, int usuarioModi)
        {
            NoCedula = noCedula;
            Rnc = rnc;
            Nombres = nombres;
            Apellidos = apellidos;
            Telefono = telefono;
            UsuarioModificador = usuarioModi;
        }
    }
}