using Entidades;
using BLL;
using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    public partial class rEmpleados : Window
    {
        Empleados empleados;
        Usuarios Modificador;
        public rEmpleados(Usuarios usuario)
        {
            InitializeComponent();
            empleados = new Empleados();
            Modificador = usuario;
            this.DataContext = empleados;

            UsuarioIdComboBox.ItemsSource = UsuariosBLL.GetList(u => true);
            UsuarioIdComboBox.SelectedValuePath = "UsuarioId";
            UsuarioIdComboBox.DisplayMemberPath = "NombreUsuario";
           
        }

        private void Limpiar()
        {
            empleados = new Empleados();
            this.DataContext = empleados;
        }

        private bool Validar()
        {
            bool valido = true;

            if (!Utilidades.Utilidades.ValidarCasillaAlfaNumerica(CodigoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El código no puede contener numeros o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CodigoTextBox.Focus();
            }
            else if (UsuarioIdComboBox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de asignar el empleado a un usuario.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                UsuarioIdComboBox.Focus();
            }
            else if (EmpleadosBLL.GetList(e => e.UsuarioId == Convert.ToInt32(UsuarioIdComboBox.SelectedValue)).FirstOrDefault() != null)
            {
                valido = false;
                MessageBox.Show("Debe de asignar el empleado a un usuario que no este asignado a un empleado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                UsuarioIdComboBox.Focus();
            }

            return valido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmpleadoIdTextBox.Text) || !Char.IsDigit(EmpleadoIdTextBox.Text[0]))
            {
                var empleados = EmpleadosBLL.Buscar(Convert.ToInt32(EmpleadoIdTextBox.Text));

                if (empleados != null)
                {
                    this.empleados = empleados;
                }
                else
                {
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("El Empleado ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                EmpleadoIdTextBox.Focus();
            }

            this.DataContext = empleados;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un empleado nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el empleado?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    int us = Convert.ToInt32(UsuarioIdComboBox.SelectedValue);
                    empleados.UsuarioModificador = Modificador.UsuarioId;
                    empleados.UsuarioId = us;
                    bool guardo = EmpleadosBLL.Guardar(empleados);

                    if (guardo)
                    {
                        Limpiar();
                        MessageBox.Show("El Empleado ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El Empleado no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el empleado?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(EmpleadoIdTextBox.Text) || !Char.IsDigit(EmpleadoIdTextBox.Text[0]))
                {
                    if (EmpleadosBLL.Eliminar(Convert.ToInt32(EmpleadoIdTextBox.Text)))
                    {
                        MessageBox.Show("El Empleado ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El Empleado no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Empleado ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    EmpleadoIdTextBox.Focus();
                }
            }
        }
    }
}
