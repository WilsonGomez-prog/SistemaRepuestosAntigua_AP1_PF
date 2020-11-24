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
    class CreditosBLL
    {
        public static bool Existe(int creditoId)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Creditos.Any(e => e.CreditoId == creditoId);
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

        public static bool Existe(int creditoId, int ClienteId)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Creditos.Any(e => e.ClienteId == ClienteId && e.CreditoId != creditoId);
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

        public static bool Insertar(Creditos credito)
        {
            bool insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Add(credito) != null)
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

        public static bool Modificar(Creditos credito)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(credito).State = EntityState.Modified;
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

        public static bool Guardar(Creditos credito)
        {

            if (!Existe(credito.CreditoId))
            {
                return Insertar(credito);
            }
            else
            {
                return Modificar(credito);
            }
        }

        public static Creditos Buscar(int creditoId)
        {
            Creditos credito;
            Contexto contexto = new Contexto();

            try
            {
                credito = contexto.Creditos.Find(creditoId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return credito;
        }

        public static bool Eliminar(int creditoId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var credito = contexto.Creditos.Find(creditoId);

                if (credito != null)
                {
                    contexto.Creditos.Remove(credito);
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

        public static List<Creditos> GetList(Expression<Func<Creditos, bool>> criterio)
        {
            List<Creditos> lista = new List<Creditos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Creditos.Where(criterio).AsNoTracking().ToList();
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
