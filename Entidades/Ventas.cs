using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un cliente que solicite la venta.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe de elegir un tipo de venta que solicite la venta.")]
        [Column(TypeName = "Bit")]
        public int TipoVenta { get; set; }

        [Required(ErrorMessage = "Debe de introducir una fecha de emisión de la factura.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe de introducir una fecha de vencimiento de la factura.")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "Debe de introducir un número de autorización.")]
        [MaxLength(8, ErrorMessage = "El número de autorización debe tener 8 caracteres.")]
        public string NoAutorizacion { get; set; }

        [Required(ErrorMessage = "Debe de introducir un NCF.")]
        [MaxLength(11, ErrorMessage = "El NCF debe tener 11 caracteres.")]
        public string Ncf { get; set; }

        [Column(TypeName = "Money")]
        public decimal Itbis { get; set; }

        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        [ForeignKey("VentaCreditoId")]
        public List<VentasDetalle> DetalleVenta { get; set; }

        public Ventas()
        {
            VentaId = 0;
            ClienteId = 0;
            Fecha = DateTime.Now;
            FechaVencimiento = DateTime.Now;
            NoAutorizacion = "";
            Ncf = "";
            Itbis = 0;
            Total = 0;
            DetalleVenta = new List<VentasDetalle>();
        }

        public Ventas(DateTime fecha, DateTime fechaVencimiento, string noAutorizacion, string ncf, decimal itbis, decimal total)
        {
            Fecha = fecha;
            FechaVencimiento = fechaVencimiento;
            NoAutorizacion = noAutorizacion;
            Ncf = ncf;
            Itbis = itbis;
            Total = total;
            DetalleVenta = new List<VentasDetalle>(); ;
        }
    }
}
