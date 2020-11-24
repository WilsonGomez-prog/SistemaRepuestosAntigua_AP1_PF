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
    class TiposProductoBLL
    {
        public static bool Existe(int TipoId)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.TiposProductos.Any(e => e.TipoProductoId == TipoId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return existe;
        }

        public static bool Existe(int TipoId, string descripcion)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.TiposProductos.Any(e => e.Descripcion == descripcion && e.TipoProductoId != TipoId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return existe;
        }

        public static bool Insertar(TiposProducto tiposProducto)
        {
            bool insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Add(tiposProducto) != null)
                    insertado = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return insertado;
        }

        public static bool Modificar(TiposProducto tiposProducto)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(tiposProducto).State = EntityState.Modified;
                modificado = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return modificado;
        }

        public static bool Guardar(TiposProducto tiposProducto)
        {

            if (!Existe(tiposProducto.TipoProductoId))
            {
                return Insertar(tiposProducto);
            }
            else
            {
                return Modificar(tiposProducto);
            }
        }

        public static TiposProducto Buscar(int tipoId)
        {
            TiposProducto tiposProducto;
            Contexto contexto = new Contexto();

            try
            {
                tiposProducto = contexto.TiposProductos.Find(tipoId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return tiposProducto;
        }

        public static bool Eliminar(int tipoId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var tiposProducto = contexto.TiposProductos.Find(tipoId);

                if (tiposProducto != null)
                {
                    contexto.TiposProductos.Remove(tiposProducto);
                    eliminado = contexto.SaveChanges() > 0;
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

            return eliminado;
        }

        public static List<TiposProducto> GetList(Expression<Func<TiposProducto, bool>> criterio)
        {
            List<TiposProducto> lista = new List<TiposProducto>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.TiposProductos.Where(criterio).AsNoTracking().ToList();
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
