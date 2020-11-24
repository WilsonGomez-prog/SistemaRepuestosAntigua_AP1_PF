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
    class VentasCreditoBLL
    {
        public static VentasCredito Buscar(int ventaCreditoId)
        {
            Contexto contexto = new Contexto();
            VentasCredito ventaCredito = new VentasCredito();

            try
            {
                ventaCredito = contexto.VentasCreditos.Include(x => x.DetalleVentaCredito).Where(p => p.VentaCreditoId == ventaCreditoId).SingleOrDefault();

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ventaCredito;
        }

        public static bool Guardar(VentasCredito ventaCredito)
        {
            bool guardado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.VentasCreditos.Add(ventaCredito) != null)
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

        public static bool Modificar(VentasCredito ventaCredito)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete from VentasCreditoDetalle where VentaCreditoId = {ventaCredito.VentaCreditoId}");
                foreach (var anterior in ventaCredito.DetalleVentaCredito)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(ventaCredito).State = EntityState.Modified;
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

        public static bool Eliminar(int ventaCreditoId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = contexto.VentasCreditos.Find(ventaCreditoId);
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

        public static List<VentasCredito> GetList(Expression<Func<VentasCredito, bool>> ventaCredito)
        {
            List<VentasCredito> lista = new List<VentasCredito>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.VentasCreditos.Where(ventaCredito).AsNoTracking().ToList();
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
