using DAL;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    class UsuariosBLL
    {
        public static bool Existe(int usuarioId)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Usuarios.Any(e => e.UsuarioId == usuarioId);
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

        public static bool Existe(int usuarioId, string username)
        {
            bool existe = false;
            Contexto contexto = new Contexto();

            try
            {
                existe = contexto.Usuarios.Any(e => e.NombreUsuario == username && e.UsuarioId != usuarioId);
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

        public static bool Insertar(Usuarios usuario)
        {
            bool insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                usuario.Clave = GetAESEncryption(usuario.Clave);

                if (contexto.Add(usuario) != null)
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

        public static bool Modificar(Usuarios usuario)
        {
            bool modificado = false;
            Contexto contexto = new Contexto();

            try
            {
                usuario.Clave = GetAESEncryption(usuario.Clave);
                contexto.Entry(usuario).State = EntityState.Modified;
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

        public static bool Guardar(Usuarios usuario)
        {

            if (!Existe(usuario.UsuarioId))
            {
                return Insertar(usuario);
            }
            else
            {
                return Modificar(usuario);
            }
        }

        public static Usuarios Buscar(int usuarioId)
        {
            Usuarios usuario;
            Contexto contexto = new Contexto();

            try
            {
                usuario = contexto.Usuarios.Find(usuarioId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }

        public static Usuarios Buscar(string usuarioNom, string pass)
        {
            Usuarios usuario = new Usuarios();
            Contexto contexto = new Contexto();

            try
            {
                usuario = contexto.Usuarios.Where(e => e.NombreUsuario == usuarioNom && e.Clave == GetAESEncryption(pass)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }

        public static bool Eliminar(int usuarioId)
        {
            bool eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var usuarios = contexto.Usuarios.Find(usuarioId);

                if (usuarios != null)
                {
                    contexto.Usuarios.Remove(usuarios);
                    EmpleadosBLL.Eliminar(EmpleadosBLL.GetList(e => e.UsuarioId == usuarioId).FirstOrDefault().EmpleadoId);
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

        public static List<Usuarios> GetList(Expression<Func<Usuarios, bool>> criterio)
        {
            List<Usuarios> lista = new List<Usuarios>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Usuarios.Where(criterio).AsNoTracking().ToList();
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

        public static bool Validar(string username, string contrasenia)
        {
            Contexto contexto = new Contexto();
            bool valido = false;

            try
            {
                valido = contexto.Usuarios.Any(u => u.NombreUsuario.Equals(username) && u.Clave.Equals(GetAESEncryption(contrasenia)));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return valido;
        }

        public static string GetAESEncryption(string contra)
        {
            byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int BlockSize = 128;

            if (contra == "")
                return null;

            byte[] bytes = Encoding.Unicode.GetBytes(contra);

            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.BlockSize = BlockSize;
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(contra));
            crypt.IV = IV;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}
