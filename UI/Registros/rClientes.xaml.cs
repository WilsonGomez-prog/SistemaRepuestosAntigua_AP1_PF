using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        Clientes Cliente;
        public rClientes()
        {
            InitializeComponent();
            Cliente = new Clientes();
            this.DataContext = Cliente;
        }

        private void Limpiar()
        {
            Cliente = new Clientes();
            this.DataContext = Cliente;
        }

        public bool Validar()
        {
            bool valido = true;

            if (!ValidarCasillaTexto(NombresTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombres no puede tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombresTextBox.Focus();
            }
            else if (!ValidarCasillaTexto(ApellidosTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla apellidos no puede tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ApellidosTextBox.Focus();
            }
            else if (!ValidarDireccion(DireccionTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla direccion no puede tener \n caracteres especiales distintos a '#', '.', ',' y '/'", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DireccionTextBox.Focus();
            }
            else if (UsuariosBLL.Existe(Convert.ToInt32(ClienteIdTextBox.Text), NoCedulaTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El cliente que esta intentando registrar ya esta regisrado\n o esta ingresando una cédula ya registrada en otro cliente.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NoCedulaTextBox.Focus();
            }
            else if (!ValidarCasillaNumerica(RNCTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El RNC no debe de contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                RNCTextBox.Focus();
            }
            else if (!ValidarCasillaNumerica(NoCedulaTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El número de cédula no debe de contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NoCedulaTextBox.Focus();
            }
            else if (!ValidarCasillaNumerica(TelefonoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El teléfono ingresado no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                TelefonoTextBox.Focus();
                //MessageBox.Show("El teléfono ingresado no está en el formato correcto,\n ingreselo con el siguiente formato: +1(###)-###-####", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return valido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var cliente = ClientesBLL.Buscar(Convert.ToInt32(ClienteIdTextBox.Text));

            if (cliente != null)
            {
                Cliente = cliente;
            }
            else
            {
                Limpiar();
            }

            this.DataContext = Cliente;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {

                bool guardo = ClientesBLL.Guardar(Cliente);

                if (guardo)
                {
                    Limpiar();
                    MessageBox.Show("El cliente ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("El cliente no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(Convert.ToInt32(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("El cliente ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("El cliente no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarCasillaNumerica(string texto)
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

        private bool ValidarCasillaTexto(string texto)
        {

            foreach (char carac in texto.ToCharArray())
            {
                if (!Char.IsLetter(carac))
                {
                    return false;
                }
            }

            return true;
        }

        //Aqui se valida que el telefono ingresado tenga el sigte formato +1(###)-###-####
        private bool ValidarTelefono(string texto)
        {
            int iterador = 0;

            foreach (char carac in texto.ToCharArray())
            {
                iterador++;
                if(iterador == 1 && carac != '+')
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

        private bool ValidarDireccion(string texto)
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
