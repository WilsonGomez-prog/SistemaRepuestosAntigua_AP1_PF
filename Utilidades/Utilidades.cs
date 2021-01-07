using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SistemaRepuestosAntigua_AP1_PF.Utilidades
{
    public class Utilidades
    {
        public static void VisualizarClave(ref Button BotonVer, ref Button BotonOcultar, ref PasswordBox password, ref TextBox Clave)
        {
            Clave.Text = password.Password;
            password.Visibility = Visibility.Hidden;
            Clave.Visibility = Visibility.Visible;
            BotonVer.Visibility = Visibility.Hidden;
            BotonOcultar.Visibility = Visibility.Visible;
        }

        public static void OcultarClave(ref Button BotonVer, ref Button BotonOcultar, ref PasswordBox password, ref TextBox Clave)
        {
            password.Password = Clave.Text;
            password.Visibility = Visibility.Visible;
            Clave.Visibility = Visibility.Hidden;
            BotonVer.Visibility = Visibility.Visible;
            BotonOcultar.Visibility = Visibility.Hidden;
        }

        public static bool ValidarUserName(string texto)
        {
            foreach (char invalido in texto.ToCharArray())
            {
                if (!Char.IsLetterOrDigit(invalido))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidarCasillaAlfaNumerica(string texto)
        {

            foreach (char carac in texto.ToCharArray())
            {
                if (!Char.IsLetterOrDigit(carac) && !Char.Equals(carac, ' ') && !Char.Equals(carac, '.'))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidarCasillaDecimal(string texto)
        {
            foreach (char invalido in texto.ToCharArray())
            {
                if (!Char.IsDigit(invalido) && !Char.Equals(invalido, '.'))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidarCasillaNumerica(string texto)
        {
            foreach (char invalido in texto.ToCharArray())
            {
                if (!Char.IsDigit(invalido))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidarCasillaTexto(string texto)
        {

            foreach (char carac in texto.ToCharArray())
            {
                if (!Char.IsLetter(carac) && !Char.Equals(carac,' '))
                {
                    return false;
                }
            }

            return true;
        }        

        public static bool ValidarDireccion(string texto)
        {

            foreach (char carac in texto.ToCharArray())
            {
                if (!Char.IsLetterOrDigit(carac) && carac != '/' && carac != '#' && carac != '.' && carac != ',' && carac != ' ')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
