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
        [MaxLength(30, ErrorMessage = "El nombre del empleado no puede exceder los 30 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un código de empleado válido.")]
        [MaxLength(30, ErrorMessage = "El apellido del empleado no puede exceder los 30 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe de ingresar un número telefónico válido.")]
        [MaxLength(11, ErrorMessage = "El número telefónico no puede exceder los 11 caracteres.")]
        public string Telefono { get; set; }

        public Clientes()
        {
            ClienteId = 0;
            NoCedula = "";
            Rnc = "";
            Nombres = "";
            Apellidos = "";
            Telefono = "";
        }

        public Clientes(string noCedula, string rnc, string nombres, string apellidos, string telefono)
        {
            NoCedula = noCedula;
            Rnc = rnc;
            Nombres = nombres;
            Apellidos = apellidos;
            Telefono = telefono;
        }
    }
}