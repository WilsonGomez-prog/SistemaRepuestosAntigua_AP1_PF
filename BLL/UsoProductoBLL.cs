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
    class UsoProductoBLL
    {
        public static List<UsoProducto> GetList(Expression<Func<UsoProducto, bool>> criterio)
        {
            List<UsoProducto> lista = new List<UsoProducto>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.UsoProducto.Where(criterio).AsNoTracking().ToList();
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
