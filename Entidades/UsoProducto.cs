using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class UsoProducto
    {
        [Key]
        public int UsoProductoId { get; set; }

        [Required(ErrorMessage = "Debe de introducir una descripcion del tipo válida.")]
        [MaxLength(20, ErrorMessage = "La descripción no puede exceder los 20 caracteres.")]
        public string Descripcion { get; set; }

        public UsoProducto()
        {
            UsoProductoId = 0;
            Descripcion = "";
        }

        public UsoProducto(int usoProductoId, string descripcion)
        {
            UsoProductoId = usoProductoId;
            Descripcion = descripcion;
        }
    }
}
