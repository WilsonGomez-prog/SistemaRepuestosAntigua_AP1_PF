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
    class ProductosBLL
    {
        public static bool Existe(int productoId)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Productos.Any(e => e.ProductoId == productoId);
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

        public static bool Existe(int productoId, string Codigo)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Productos.Any(e => e.Codigo == Codigo && e.ProductoId != productoId);
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

        public static bool Insertar(Productos producto)
        {
            bool insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Add(producto) != null)
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

        public static bool Modificar(Productos producto)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(producto).State = EntityState.Modified;
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

        public static bool Guardar(Productos producto)
        {

            if (!Existe(producto.ProductoId))
            {
                return Insertar(producto);
            }
            else
            {
                return Modificar(producto);
            }
        }

        public static Productos Buscar(int productoId)
        {
            Productos producto;
            Contexto contexto = new Contexto();

            try
            {
                producto = contexto.Productos.Find(productoId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return producto;
        }

        public static bool Eliminar(int productoId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var producto = contexto.Productos.Find(productoId);

                if (producto != null)
                {
                    contexto.Productos.Remove(producto);
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

        public static List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {
            List<Productos> lista = new List<Productos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Productos.Where(criterio).AsNoTracking().ToList();
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
