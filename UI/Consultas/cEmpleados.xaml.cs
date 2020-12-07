using Entidades;
using BLL;
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

namespace SistemaRepuestosAntigua_AP1_PF.UI.Consultas
{
    
    public partial class cEmpleados : Window
    {
        public cEmpleados()
        {
            InitializeComponent();
        }

        private List<dynamic> GetDisplay(List<Empleados> lista)
        {
            var listado = new List<dynamic>();

            foreach (var empleado in lista)
            {
                var emp = new
                {
                    empleado.EmpleadoId,
                    empleado.Codigo,
                    UsuariosBLL.Buscar(empleado.UsuarioId).NombreUsuario,
                    UsuarioModificador = UsuariosBLL.Buscar(empleado.UsuarioModificador).NombreUsuario 
                };

                listado.Add(emp);
            }

            return listado;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            List<Empleados> listado = new List<Empleados>();
            List<dynamic> list = new List<dynamic>();
            try
            {

                if (!string.IsNullOrEmpty(CriterioTextBox.Text))
                {
                    if (FiltroComboBox.SelectedItem != null)
                    {
                        switch (FiltroComboBox.SelectedIndex)
                        {

                            case 1:
                                listado = EmpleadosBLL.GetList(p => p.EmpleadoId == int.Parse(CriterioTextBox.Text));
                                list = GetDisplay(listado);
                                break;

                            case 2:
                                listado = EmpleadosBLL.GetList(p => p.Codigo == CriterioTextBox.Text);
                                list = GetDisplay(listado);
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No puede elvaluar por criterio si no ha seleccionado un filtro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                        FiltroComboBox.Focus();
                    }
                }
                else
                {
                    listado = EmpleadosBLL.GetList(c => true);
                    list = GetDisplay(listado);
                }
            }
            catch (Exception)
            {
                throw;
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = list;


        }
    }
}
