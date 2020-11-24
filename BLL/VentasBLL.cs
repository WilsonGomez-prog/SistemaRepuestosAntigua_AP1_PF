using DAL;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
    }
}
