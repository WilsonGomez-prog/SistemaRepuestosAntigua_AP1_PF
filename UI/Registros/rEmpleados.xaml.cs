using Entidades;
using BLL;
using DAL;
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
    public partial class rEmpleados : Window
    {
        Empleados empleados;
        public rEmpleados()
        {
            InitializeComponent();
            empleados = new Empleados();
            Contexto contexto = new Contexto();
            this.DataContext = empleados;

            UsuarioIdComboBox.ItemsSource = UsuariosBLL.GetList(u => true);
            UsuarioIdComboBox.SelectedValuePath = "UsuarioId";
            UsuarioIdComboBox.DisplayMemberPath = "Nombres";
           
        }

        private void Limpiar()
        {
            empleados = new Empleados();
            this.DataContext = empleados;
        }

        private bool Validar()
        {
            bool valido = true;

            if (!ValidarCasillaTexto(CodigoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El Codigo no puede contener numeros o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CodigoTextBox.Focus();
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
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
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

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
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
    }
}
