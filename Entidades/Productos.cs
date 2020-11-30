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
        public float PrecioUnit { get; set; }

        [Required(ErrorMessage = "Debe de introducir un porciento de descuento válido.")]
        public float Descuento { get; set; }

        [Required(ErrorMessage = "Debe de introducir un código de producto válido.")]
        [MaxLength(13, ErrorMessage = "El código de producto no puede exceder los 13 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Debe de especificar el tipo de producto.")]
        public int TipoProductoId { get; set; }

        [Required(ErrorMessage = "Debe de especificar el estado de uso del producto.")]
        [Column(TypeName = "Bit")]
        public int EstadoProducto { get; set; }

        [Required(ErrorMessage = "Debe de especificar la cantidad de existencias que hay del producto.")]
        public float Existencia { get; set; }

        [Required(ErrorMessage = "Debe de especificar el porciento de impuesto que se le aplica al producto.")]
        public float Impuesto { get; set; }

        [Required(ErrorMessage = "Debe de indicar el id del usuario que lo modifico por ultima vez.")]
        public int UsuarioModificador { get; set; }

        public Productos()
        {
            ProductoId = 0;
            Descripcion = "";
            PrecioUnit = 0;
            Descuento = 0;
            Codigo = "";
            TipoProductoId = 0;
            EstadoProducto = 0;
            Existencia = 0;
            UsuarioModificador = 0;
        }

        public Productos(string descripcion, float precioUnit, float descuento, string codigo, int tipoProductoId, int estadoProducto, float existencia)
        {
            Descripcion = descripcion;
            PrecioUnit = precioUnit;
            Descuento = descuento;
            Codigo = codigo;
            TipoProductoId = tipoProductoId;
            EstadoProducto = estadoProducto;
            Existencia = existencia;
        }
    }
}
