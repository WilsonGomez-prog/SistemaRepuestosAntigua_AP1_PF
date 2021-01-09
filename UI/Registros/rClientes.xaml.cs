using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        Clientes Cliente;
        Usuarios Modificador;
        public rClientes(Usuarios usuario)
        {
            InitializeComponent();
            Cliente = new Clientes();
            this.DataContext = Cliente;
            Modificador = usuario;
        }

        private void Limpiar()
        {
            Cliente = new Clientes();
            this.DataContext = Cliente;
        }

        public bool Validar()
        {

            if (!ValidarAdvertencia())
            {
                MessageBox.Show("El cliente no se pudo guardar, revise las advertencias.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!ValidarContexto())
            {
                MessageBox.Show("El formulario no ha sido completado, no se puede guardar.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ClienteIdTextBox.Text) || !Char.IsDigit((char)ClienteIdTextBox.Text[0]))
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
            else
            {
                MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClienteIdTextBox.Focus();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un cliente nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el cliente?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    Cliente.UsuarioModificador = Modificador.UsuarioId;
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
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el cliente?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(ClienteIdTextBox.Text) || !Char.IsDigit((char)ClienteIdTextBox.Text[0]) || Convert.ToInt32(ClienteIdTextBox.Text) == 0)
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
                else
                {
                    MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClienteIdTextBox.Focus();
                }
            }
        }

        private void NombresTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaTexto(NombresTextBox.Text) || string.IsNullOrWhiteSpace(NombresTextBox.Text))
            {
                NombreVad.Text = "La casilla nombres no puede estar vacía o tener números ni caracteres especiales.";
                NombreVad.Visibility = Visibility.Visible;
                NombresTextBox.Focus();
            }
            else
            {
                NombreVad.Visibility = Visibility.Hidden;
            }
        }

        private void ApellidosTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaTexto(ApellidosTextBox.Text) || string.IsNullOrWhiteSpace(ApellidosTextBox.Text))
            {
                ApellidoVad.Text = "La casilla apellidos no puede estar vacío o tener números ni caracteres especiales.";
                ApellidoVad.Visibility = Visibility.Visible;
                ApellidosTextBox.Focus();
            }
            else
            {
                ApellidoVad.Visibility = Visibility.Hidden;
            }
        }

        private void NoCedulaTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaNumerica(NoCedulaTextBox.Text) || string.IsNullOrWhiteSpace(NoCedulaTextBox.Text))
            {
                CedulaVad.Text = "El número de cédula no debe estar vacío o contener letras o caracteres especiales.";
                CedulaVad.Visibility = Visibility.Visible;
                DireccionTextBox.Focus();
            }
            else if (UsuariosBLL.Existe(Convert.ToInt32(ClienteIdTextBox.Text), NoCedulaTextBox.Text))
            {
                CedulaVad.Text = "El cliente que esta intentando registrar ya esta regisrado\n o esta ingresando una cédula ya registrada en otro cliente.";
                CedulaVad.Visibility = Visibility.Visible;
                NoCedulaTextBox.Focus();
            }
            else
            {
                CedulaVad.Visibility = Visibility.Hidden;
            }
        }

        private void RNCTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaNumerica(RNCTextBox.Text) || string.IsNullOrWhiteSpace(RNCTextBox.Text))
            {
                RNCVad.Text = "El RNC no debe estar vacío o contener letras o caracteres especiales.";
                RNCVad.Visibility = Visibility.Visible;
                RNCTextBox.Focus();
            }
            else
            {
                RNCVad.Visibility = Visibility.Hidden;
            }
        }

        private void TelefonoTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaNumerica(TelefonoTextBox.Text) || string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
            {
                TelefonoVad.Text = "El teléfono ingresado no puede estar vacío o contener letras o caracteres especiales.";
                TelefonoVad.Visibility = Visibility.Visible;
                TelefonoTextBox.Focus();
            }
            else
            {
                TelefonoVad.Visibility = Visibility.Hidden;
            }
        }

        private void DireccionTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarDireccion(DireccionTextBox.Text) || string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                DireccionVad.Text = "La casilla direccion no puede estar vacío o tener \n caracteres especiales distintos a '#', '.', ',' y '/'";
                DireccionVad.Visibility = Visibility.Visible;
                DireccionTextBox.Focus();
            }
            else
            {
                DireccionVad.Visibility = Visibility.Hidden;
            }
        }

        private bool ValidarContexto()
        {
            List<TextBox> Controles = new List<TextBox> { NombresTextBox, ApellidosTextBox, NoCedulaTextBox, RNCTextBox, DireccionTextBox, TelefonoTextBox};

            foreach (TextBox control in Controles)
            {
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidarAdvertencia()
        {
            List<TextBlock> ErrorMessages = new List<TextBlock> { NombreVad, ApellidoVad, CedulaVad, DireccionVad, RNCVad, TelefonoVad };

            foreach (TextBlock error in ErrorMessages)
            {
                if (error.Visibility == Visibility.Visible)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
