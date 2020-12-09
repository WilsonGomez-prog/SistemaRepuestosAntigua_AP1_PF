using DAL;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    class CobrosBLL
    {
        public static Cobros Buscar(int cobroId)
        {
            Contexto contexto = new Contexto();
            Cobros cobro = new Cobros();

            try
            {
                cobro = contexto.Cobros.Include(x => x.DetalleCobro).Where(p => p.CobroId == cobroId).SingleOrDefault();

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return cobro;
        }

        public static bool Guardar(Cobros cobro)
        {
            bool guardado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Cobros.Add(cobro) != null)
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

        public static bool Modificar(Cobros cobro)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete from CobrosDetalle where CobroId = {cobro.CobroId}");
                foreach (var anterior in cobro.DetalleCobro)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(cobro).State = EntityState.Modified;
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

        public static bool Eliminar(int cobroId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                RestaurarCuentas(Buscar(cobroId));
                var eliminar = contexto.Cobros.Find(cobroId);
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

        public static void RestaurarCuentas(Cobros cobro)
        {
            Contexto contexto = new Contexto();

            try
            {
                if (cobro != null)
                {
                    foreach (var detalle in cobro.DetalleCobro)
                    {
                        var venta = VentasBLL.Buscar(detalle.VentaId);

                        if (venta != null)
                        {
                            contexto.Database.ExecuteSqlRaw($"Update Ventas set PendientePagar = PendientePagar + {detalle.Monto} where VentaId = {detalle.VentaId}");
                        }
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

        public static List<Cobros> GetList(Expression<Func<Cobros, bool>> cobro)
        {
            List<Cobros> lista = new List<Cobros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Cobros.Where(cobro).AsNoTracking().ToList();
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
