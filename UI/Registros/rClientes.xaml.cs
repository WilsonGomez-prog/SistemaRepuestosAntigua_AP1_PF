using BLL;
using Entidades;
using System;
using System.Windows;

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
            bool valido = true;

            if (!Utilidades.Utilidades.ValidarCasillaTexto(NombresTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombres no puede tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombresTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaTexto(ApellidosTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla apellidos no puede tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ApellidosTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarDireccion(DireccionTextBox.Text))
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
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(RNCTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El RNC no debe de contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                RNCTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(NoCedulaTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El número de cédula no debe de contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NoCedulaTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(TelefonoTextBox.Text))
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
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un cliente nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el cliente?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
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
            if (MessageBox.Show("¿De verdad desea eliminar el cliente?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(ClienteIdTextBox.Text) || !Char.IsDigit((char)ClienteIdTextBox.Text[0]))
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
    }
}
