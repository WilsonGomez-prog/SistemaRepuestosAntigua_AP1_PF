using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Debe de introducir una descripcion del tipo válida.")]
        [MaxLength(20, ErrorMessage = "La descripción del producto no puede exceder los 20 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe de introducir un precio válido del producto.")]
        [Column(TypeName = "Money")]
        public decimal PrecioUnit { get; set; }

        [Required(ErrorMessage = "Debe de introducir un porciento de descuento válido.")]
        public float Descuento { get; set; }

        [Required(ErrorMessage = "Debe de introducir un código de producto válido.")]
        [MaxLength(13, ErrorMessage = "El código de producto no puede exceder los 13 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Debe de especificar el tipo de producto.")]
        public int TipoProductoId { get; set; }

        [Required(ErrorMessage = "Debe de especificar el estado de uso del producto.")]
        public int UsoProductoId { get; set; }

        [Required(ErrorMessage = "Debe de especificar la cantidad de existencias que hay del producto.")]
        public float Existencia { get; set; }

        public Productos()
        {
            ProductoId = 0;
            Descripcion = "";
            PrecioUnit = 0;
            Descuento = 0;
            Codigo = "";
            TipoProductoId = 0;
            UsoProductoId = 0;
            Existencia = 0;
        }

        public Productos(string descripcion, decimal precioUnit, float descuento, string codigo, int tipoProductoId, int usoProductoId, float existencia)
        {
            Descripcion = descripcion;
            PrecioUnit = precioUnit;
            Descuento = descuento;
            Codigo = codigo;
            TipoProductoId = tipoProductoId;
            UsoProductoId = usoProductoId;
            Existencia = existencia;
        }
    }
}
