using BLL;
using Entidades;
using System;
using System.Collections.Generic;
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

            List<TextBlock> ErrorMessages = new List<TextBlock> { FechaVad, NombreVad, ApellidoVad, UserNameVad, PermisosVad, ContraVad, VerificacionVad };
            List<TextBox> Controles = new List<TextBox> {NombreTextBox, ApellidoTextBox, NombreUsuarioTextBox, ClaveTextBox, ClaveVerificacionTextBox };

            foreach (TextBlock error in ErrorMessages)
            {
                if (error.Visibility == Visibility.Visible)
                {
                    valido = false;
                    break;
                }
            }

            foreach (TextBox control in Controles)
            {
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    valido = false;
                    MessageBox.Show("El formulario no ha sido completado, no se puede guardar.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
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
            Utilidades.Utilidades.VisualizarClave(ref VisualizarClaveButton, ref OcultarClaveButton, ref ClavePasswordBox, ref ClaveTextBox);
        }

        private void OcultarClaveButton_Click(object sender, RoutedEventArgs e)
        {
            Utilidades.Utilidades.OcultarClave(ref VisualizarClaveButton, ref OcultarClaveButton, ref ClavePasswordBox, ref ClaveTextBox);
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

            ValidarClave();
        }


        private void ClaveVerificacionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClaveVerificacionPasswordBox.Password = ClaveVerificacionTextBox.Text;

            ValidarVerificacion();
        }

        private void FechaDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FechaDatePicker.SelectedDate > DateTime.Now)
            {
                FechaVad.Visibility = Visibility.Visible;
                FechaDatePicker.Focus();
            }
            else
            {
                FechaVad.Visibility = Visibility.Hidden;
            }
        }

        private void NombreTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaTexto(NombreTextBox.Text) || string.IsNullOrWhiteSpace(NombreTextBox.Text))
            {
                NombreVad.Visibility = Visibility.Visible;
                NombreTextBox.Focus();
            }
            else
            {
                NombreVad.Visibility = Visibility.Hidden;
            }
        }

        private void ApellidoTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarCasillaTexto(ApellidoTextBox.Text) || string.IsNullOrWhiteSpace(ApellidoTextBox.Text))
            {
                ApellidoVad.Visibility = Visibility.Visible;
                ApellidoTextBox.Focus();
            }
            else
            {
                ApellidoVad.Visibility = Visibility.Hidden;
            }
        }


        private void EsAdminCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EsAdminCombobox.SelectedItem == null)
            {
                PermisosVad.Visibility = Visibility.Visible;
                EsAdminCombobox.Focus();
            }
            else
            {
                PermisosVad.Visibility = Visibility.Hidden;
            }
        }

        private void NombreUsuarioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Utilidades.Utilidades.ValidarUserName(NombreUsuarioTextBox.Text) || string.IsNullOrWhiteSpace(NombreUsuarioTextBox.Text))
            {
                UserNameVad.Text = "El nombre de usuario no puede tener simbolos especiales o estar vacío";
                UserNameVad.Visibility = Visibility.Visible;
                NombreUsuarioTextBox.Focus();
            }
            else if (UsuariosBLL.Existe(Convert.ToInt32(UsuarioIdTextBox.Text), NombreUsuarioTextBox.Text))
            {
                UserNameVad.Text = "El nombre de usuario ingresado en la casilla 'Nombre de usuario' ya pertenece a otro usuario, ingrese uno diferente.";
                UserNameVad.Visibility = Visibility.Visible;
                NombreUsuarioTextBox.Focus();
            }
            else
            {
                UserNameVad.Visibility = Visibility.Hidden;
            }
        }

        private void ClavePasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidarClave();
        }

        private void ClaveVerificacionPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidarVerificacion();
        }

        private void ValidarClave()
        {
            if (string.IsNullOrWhiteSpace(ClavePasswordBox.Password) || ClavePasswordBox.Password.Length < 6)
            {
                ContraVad.Text = "Es necesario ingresar una clave con un mínimo de 6 caracteres.";
                ContraVad.Visibility = Visibility.Visible;
                ClaveTextBox.Focus();
            }
            else
            {
                ContraVad.Visibility = Visibility.Hidden;
            }
        }

        private void ValidarVerificacion()
        {
            if (string.IsNullOrWhiteSpace(ClaveVerificacionPasswordBox.Password))
            {
                VerificacionVad.Text = "Es necesario es necesario verificar la clave ingresada.";
                VerificacionVad.Visibility = Visibility.Visible;
                ClaveVerificacionPasswordBox.Focus();
            }
            else if (ClaveVerificacionPasswordBox.Password != ClavePasswordBox.Password)
            {
                VerificacionVad.Text = "La verificación no coincide con la clave ingresada.";
                VerificacionVad.Visibility = Visibility.Visible;
                ClaveVerificacionPasswordBox.Focus();
            }
            else
            {
                VerificacionVad.Visibility = Visibility.Hidden;
            }
        }
    }
}
