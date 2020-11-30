using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaRepuestosAntigua_AP1_PF.Utilidades
{
    public class Utilidades
    {
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

        public static bool ValidarCasillaMonetaria(string texto)
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

        //Aqui se valida que el telefono ingresado tenga el sigte formato +1(###)-###-####
        public static bool ValidarTelefono(string texto)
        {
            int iterador = 0;

            foreach (char carac in texto.ToCharArray())
            {
                iterador++;
                if (iterador == 1 && carac != '+')
                {
                    return false;
                }
                else if (iterador == 2 && carac != '1')
                {
                    return false;
                }
                else if (iterador == 3 && carac != '(')
                {
                    return false;
                }
                else if (iterador == 7 && carac != ')')
                {
                    return false;
                }
                else if (iterador == 8 && carac != '-')
                {
                    return false;
                }
                else if (iterador == 12 && carac != '-')
                {
                    return false;
                }
                else if (!Char.IsDigit(carac))
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
