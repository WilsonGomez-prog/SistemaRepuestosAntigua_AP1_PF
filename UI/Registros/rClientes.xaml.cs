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

            if (!Utilidades.Utilidades.ValidarCasillaTexto(NombresTextBox.Text) || string.IsNullOrWhiteSpace(NombresTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombres no puede estar vacío o tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombresTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaTexto(ApellidosTextBox.Text) || string.IsNullOrWhiteSpace(ApellidosTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla apellidos no puede estar vacío o tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ApellidosTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarDireccion(DireccionTextBox.Text) || string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla direccion no puede estar vacío o tener \n caracteres especiales distintos a '#', '.', ',' y '/'", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DireccionTextBox.Focus();
            }
            else if (UsuariosBLL.Existe(Convert.ToInt32(ClienteIdTextBox.Text), NoCedulaTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El cliente que esta intentando registrar ya esta regisrado\n o esta ingresando una cédula ya registrada en otro cliente.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NoCedulaTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(RNCTextBox.Text) || string.IsNullOrWhiteSpace(RNCTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El RNC no debe estar vacío o contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                RNCTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(NoCedulaTextBox.Text) || string.IsNullOrWhiteSpace(NoCedulaTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El número de cédula no debe estar vacío o contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NoCedulaTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(TelefonoTextBox.Text) || string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El teléfono ingresado no puede estar vacío o contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                TelefonoTextBox.Focus();
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
    }
}
