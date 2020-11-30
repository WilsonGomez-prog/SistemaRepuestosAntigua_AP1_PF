using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class TiposProducto
    {
        [Key]
        public int TipoProductoId { get; set; }

        [Required(ErrorMessage = "Debe de introducir una descripcion del tipo válida.")]
        [MaxLength(20, ErrorMessage = "La descripción no puede exceder los 20 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        public TiposProducto()
        {
            TipoProductoId = 0;
            Descripcion = "";
            UsuarioModificador = 0;
        }

        public TiposProducto(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}
