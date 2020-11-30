using BLL;
using Entidades;
using System;
using System.Windows;

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
            ClaveTextBox.Password = string.Empty;
            ClaveVerificacionTextBox.Password = string.Empty;
            this.DataContext = Usuario;
        }

        private bool Validar()
        {
            bool valido = true;

            if(!Utilidades.Utilidades.ValidarCasillaTexto(NombreTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombre no puede tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombreTextBox.Focus();
            }
            else if(!Utilidades.Utilidades.ValidarCasillaTexto(ApellidoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla apellido no puede tener \nnumeros ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ApellidoTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarUserName(NombreUsuarioTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla nombre de usuario no puede tener \n caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombreUsuarioTextBox.Focus();
            }
            else if (UsuariosBLL.Existe(Convert.ToInt32(UsuarioIdTextBox.Text), NombreUsuarioTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El nombre de usuario ingresado en la casilla\n'Nombre de usuario' ya pertenece a otro usuario,\n ingrese uno diferente.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                NombreUsuarioTextBox.Focus();
            }
            else if(string.IsNullOrWhiteSpace(ClaveTextBox.Password) || ClaveTextBox.Password.Length < 6)
            {
                valido = false;
                MessageBox.Show("Es necesario ingresar una clave con un minimo de 6 caracteres.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClaveTextBox.Clear();
                ClaveTextBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(ClaveVerificacionTextBox.Password))
            {
                valido = false;
                MessageBox.Show("Es necesario es necesario verificar la clave ingresada.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClaveTextBox.Clear();
                ClaveTextBox.Focus();
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
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un usuario nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el usuario?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    Usuario.Clave = ClaveTextBox.Password;
                    Usuario.UsuarioModificador = Modificador.UsuarioId;
                    Usuario.Fecha = Convert.ToDateTime(FechaDatePicker.SelectedDate.Value.Date.ToShortDateString());

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
            if (MessageBox.Show("¿De verdad desea eliminar el usuario? Tambien va a eliminar al \nempleado de la base de datos que este asignado al usuario.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
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
        }
    }
}
