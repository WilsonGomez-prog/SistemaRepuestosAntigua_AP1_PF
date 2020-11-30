using DAL;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;

namespace BLL
{
    class VentasBLL
    {
        public static Ventas Buscar(int ventaId)
        {
            Contexto contexto = new Contexto();
            Ventas venta = new Ventas();

            try
            {
                venta = contexto.Ventas.Include(x => x.DetalleVenta).Where(p => p.VentaId == ventaId).SingleOrDefault();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return venta;
        }

        public static bool Guardar(Ventas venta)
        {
            bool guardado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Ventas.Add(venta) != null)
                {
                    guardado = contexto.SaveChanges() > 0;
                    AlterarCredito(venta);
                    AlterarInventario(venta);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return guardado;
        }

        public static bool Modificar(Ventas venta)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete from VentasDetalle where VentaId = {venta.VentaId}");
                foreach (var anterior in venta.DetalleVenta)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(venta).State = EntityState.Modified;
                AlterarCredito(venta);
                AlterarInventario(venta);
                modificado = contexto.SaveChanges() > 0;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return modificado;
        }

        public static void CobrarVentaCredito(Cobros Cobro)
        {
            Contexto contexto = new Contexto();
            try
            {
                foreach (CobrosDetalle detalle in Cobro.DetalleCobro)
                {
                    if (detalle.EstaPago == false)
                    {
                        contexto.Database.ExecuteSqlRaw($"Update Ventas set PendientePagar = PendientePagar - {detalle.Monto} where VentaId = {detalle.VentaId}");
                        detalle.EstaPago = true;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
        }

        public static bool Eliminar(int ventaId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = contexto.Ventas.Find(ventaId);
                contexto.Entry(eliminar).State = EntityState.Deleted;

                eliminado = contexto.SaveChanges() > 0;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return eliminado;
        }

        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> venta)
        {
            List<Ventas> lista = new List<Ventas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Ventas.Where(venta).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static void AlterarCredito(Ventas venta)
        {
            try
            {
                if (venta != null && venta.TipoVenta == 1)
                {
                    var credito = CreditosBLL.BuscarPorCliente(venta.ClienteId);

                    if (credito != null)
                    {
                        credito.Balance -= venta.Total;
                        CreditosBLL.Guardar(credito);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AlterarInventario(Ventas venta)
        {
            try
            {
                if (venta != null)
                {
                    foreach (var detalle in venta.DetalleVenta)
                    {
                        var producto = ProductosBLL.Buscar(detalle.ProductoId);

                        if (producto != null)
                        {
                            producto.Existencia -= detalle.Cantidad;
                            ProductosBLL.Guardar(producto);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
