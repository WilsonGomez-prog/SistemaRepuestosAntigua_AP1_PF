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
    class VentasContadoBLL
    {
        public static VentasContado Buscar(int ventaContadoId)
        {
            Contexto contexto = new Contexto();
            VentasContado ventaContado = new VentasContado();

            try
            {
                ventaContado = contexto.VentasContados.Include(x => x.DetalleVentaContado).Where(p => p.VentaContadoId == ventaContadoId).SingleOrDefault();

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ventaContado;
        }

        public static bool Guardar(VentasContado ventaContado)
        {
            bool guardado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.VentasContados.Add(ventaContado) != null)
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

        public static bool Modificar(VentasContado ventaContado)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete from VentasContadoDetalle where VentaContadoId = {ventaContado.VentaContadoId}");
                foreach (var anterior in ventaContado.DetalleVentaContado)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(ventaContado).State = EntityState.Modified;
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

        public static bool Eliminar(int ventaContadoId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = contexto.VentasContados.Find(ventaContadoId);
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

        public static List<VentasContado> GetList(Expression<Func<VentasContado, bool>> ventaContado)
        {
            List<VentasContado> lista = new List<VentasContado>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.VentasContados.Where(ventaContado).AsNoTracking().ToList();
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
