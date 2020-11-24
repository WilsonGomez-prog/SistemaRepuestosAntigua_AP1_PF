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
    class EmpleadosBLL
    {
        public static bool Existe(int empleadoId)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Empleados.Any(e => e.EmpleadoId == empleadoId);
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

        public static bool Existe(int empleadoId, string Codigo)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Empleados.Any(e => e.Codigo == Codigo && e.EmpleadoId != empleadoId);
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

        public static bool Insertar(Empleados empleado)
        {
            bool insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Add(empleado) != null)
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

        public static bool Modificar(Empleados empleado)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(empleado).State = EntityState.Modified;
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

        public static bool Guardar(Empleados empleado)
        {

            if (!Existe(empleado.EmpleadoId))
            {
                return Insertar(empleado);
            }
            else
            {
                return Modificar(empleado);
            }
        }

        public static Empleados Buscar(int empleadoId)
        {
            Empleados empleado;
            Contexto contexto = new Contexto();

            try
            {
                empleado = contexto.Empleados.Find(empleadoId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return empleado;
        }

        public static bool Eliminar(int empleadoId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var empleado = contexto.Empleados.Find(empleadoId);

                if (empleado != null)
                {
                    contexto.Empleados.Remove(empleado);
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

        public static List<Empleados> GetList(Expression<Func<Empleados, bool>> criterio)
        {
            List<Empleados> lista = new List<Empleados>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Empleados.Where(criterio).AsNoTracking().ToList();
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
