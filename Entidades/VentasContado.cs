using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class VentasContado
    {
        [Key]
        public int VentaContadoId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un cliente que solicite la venta.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe de introducir una fecha de emisión de la factura.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe de introducir una condición válida.")]
        [MaxLength(8, ErrorMessage = "La condicion debe tener hasta 8 caracteres.")]
        public string Condicion { get; set; }

        [Column(TypeName = "Money")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "Money")]
        public decimal Itbis { get; set; }

        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        [ForeignKey("VentaContadoId")]
        public List<VentasContadoDetalle> DetalleVentaContado { get; set; }

        public VentasContado()
        {
            VentaContadoId = 0;
            ClienteId = 0;
            Fecha = DateTime.Now;
            Condicion = "";
            SubTotal = 0;
            Itbis = 0;
            Total = 0;
            DetalleVentaContado = new List<VentasContadoDetalle>();
        }

        public VentasContado(int clienteId, DateTime fecha, string condicion, decimal subTotal, decimal itbis, decimal total)
        {
            ClienteId = clienteId;
            Fecha = fecha;
            Condicion = condicion;
            SubTotal = subTotal;
            Itbis = itbis;
            Total = total;
            DetalleVentaContado = new List<VentasContadoDetalle>();
        }
    }
}
