using BLL;
using Entidades;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        Usuarios Usuario;
        Usuarios Modificador;
        public rUsuarios(Usuarios usuario)
        {
            InitializeComponent();
            Usuario = new Usuarios();
            this.DataContext = Usuario;
            Modificador = usuario;
        }

        private void Limpiar()
        {
            Usuario = new Usuarios();
            ClavePasswordBox.Password = ClaveTextBox.Text = string.Empty;
            ClaveVerificacionPasswordBox.Password = ClaveVerificacionTextBox.Text = string.Empty;
            this.DataContext = Usuario;
        }

        private bool Validar()
        {
            bool valido = true;

            if(!Utilidades.Utilidades.ValidarCasillaTexto(NombreTextBox.Text) || string.IsNullOrWhiteSpace(NombreTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombre no puede tener \nnumeros ni caracteres especiales o estar vacía.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombreTextBox.Focus();
            }
            else if(!Utilidades.Utilidades.ValidarCasillaTexto(ApellidoTextBox.Text) || string.IsNullOrWhiteSpace(ApellidoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla apellido no puede tener \nnumeros ni caracteres especiales o estar vacía.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ApellidoTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarUserName(NombreUsuarioTextBox.Text) || string.IsNullOrWhiteSpace(NombreUsuarioTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombre de usuario no puede tener \n caracteres especiales o estar vacía.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombreUsuarioTextBox.Focus();
            }
            else if (EsAdminCombobox.SelectedItem  == null)
            {
                valido = false;
                MessageBox.Show("Debe asignarle un tipo de permiso al usuario.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                EsAdminCombobox.Focus();
            }
            else if (UsuariosBLL.Existe(Convert.ToInt32(UsuarioIdTextBox.Text), NombreUsuarioTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El nombre de usuario ingresado en la casilla\n'Nombre de usuario' ya pertenece a otro usuario,\n ingrese uno diferente.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombreUsuarioTextBox.Focus();
            }
            else if(string.IsNullOrWhiteSpace(ClavePasswordBox.Password) || ClavePasswordBox.Password.Length < 6)
            {
                valido = false;
                MessageBox.Show("Es necesario ingresar una clave con un minimo de 6 caracteres.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClaveTextBox.Clear();
                ClaveVerificacionPasswordBox.Clear();
                ClaveTextBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(ClaveVerificacionPasswordBox.Password))
            {
                valido = false;
                MessageBox.Show("Es necesario es necesario verificar la clave ingresada.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClaveTextBox.Clear();
                ClaveVerificacionPasswordBox.Clear();
                ClaveTextBox.Focus();
            }
            else if (ClaveVerificacionPasswordBox.Password != ClavePasswordBox.Password)
            {
                valido = false;
                MessageBox.Show("La verificación no coincide con la clave ingresada.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClaveVerificacionPasswordBox.Clear();
                ClaveVerificacionPasswordBox.Focus();
            }
            else if (FechaDatePicker.SelectedDate > DateTime.Now)
            {
                valido = false;
                MessageBox.Show("No se puede elegir una fecha mayor a la actual.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                FechaDatePicker.Focus();
            }

            return valido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var usuario = UsuariosBLL.Buscar(Convert.ToInt32(UsuarioIdTextBox.Text));

            if(usuario != null)
            {
                Usuario = usuario;
            }
            else
            {
                Limpiar();
            }

            this.DataContext = Usuario;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un usuario nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el usuario?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    Usuario.Clave = ClavePasswordBox.Password;
                    Usuario.UsuarioModificador = Modificador.UsuarioId;
                    Usuario.Fecha = Convert.ToDateTime(FechaDatePicker.SelectedDate.Value.Date.ToShortDateString());
                    Usuario.EsAdmin = EsAdminCombobox.SelectedIndex != -1 && EsAdminCombobox.SelectedIndex == 1 ? 1 : 0;

                    bool guardo = UsuariosBLL.Guardar(Usuario);

                    if (guardo)
                    {
                        Limpiar();
                        MessageBox.Show("El usuario ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El usuario no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el usuario? Tambien va a eliminar al \nempleado de la base de datos que este asignado al usuario.", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(UsuarioIdTextBox.Text) || !Char.IsDigit((char)UsuarioIdTextBox.Text[0]) || Convert.ToInt32(UsuarioIdTextBox.Text) == 0)
                {
                    if (UsuariosBLL.Eliminar(Convert.ToInt32(UsuarioIdTextBox.Text)))
                    {
                        MessageBox.Show("El usuario ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales o el formulario esta vacío.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    UsuarioIdTextBox.Focus();
                }
            }
        }

        private void VisualizarVerificarButton_Click(object sender, RoutedEventArgs e)
        {
            ClaveVerificacionTextBox.Text = ClaveVerificacionPasswordBox.Password;
            ClaveVerificacionPasswordBox.Visibility = Visibility.Hidden;
            ClaveVerificacionTextBox.Visibility = Visibility.Visible;
            VisualizarVerificarButton.Visibility = Visibility.Hidden;
            OcultarVerificarButton.Visibility = Visibility.Visible;
        }

        private void VisualizarClaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClaveTextBox.Text = ClavePasswordBox.Password;
            ClavePasswordBox.Visibility = Visibility.Hidden;
            ClaveTextBox.Visibility = Visibility.Visible;
            VisualizarClaveButton.Visibility = Visibility.Hidden;
            OcultarClaveButton.Visibility = Visibility.Visible;
        }

        private void OcultarClaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClavePasswordBox.Password = ClaveTextBox.Text;
            ClavePasswordBox.Visibility = Visibility.Visible;
            ClaveTextBox.Visibility = Visibility.Hidden;
            VisualizarClaveButton.Visibility = Visibility.Visible;
            OcultarClaveButton.Visibility = Visibility.Hidden;
        }

        private void OcultarVerificarButton_Click(object sender, RoutedEventArgs e)
        {
            ClaveVerificacionPasswordBox.Password = ClaveVerificacionTextBox.Text;
            ClaveVerificacionPasswordBox.Visibility = Visibility.Visible;
            ClaveVerificacionTextBox.Visibility = Visibility.Hidden;
            VisualizarVerificarButton.Visibility = Visibility.Visible;
            OcultarVerificarButton.Visibility = Visibility.Hidden;
        }


        private void ClaveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClavePasswordBox.Password = ClaveTextBox.Text;
        }


        private void ClaveVerificacionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClaveVerificacionPasswordBox.Password = ClaveVerificacionTextBox.Text;
        }
    }
}
