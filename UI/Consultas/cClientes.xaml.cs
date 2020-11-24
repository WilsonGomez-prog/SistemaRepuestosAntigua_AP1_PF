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

namespace SistemaRepuestosAntigua_AP1_PF.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cClientes.xaml
    /// </summary>
    public partial class cClientes : Window
    {
        public cClientes()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Clientes>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                listado = ClientesBLL.GetList(e => true);
            }
            else
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = ClientesBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                        break;
                    case 1:
                        listado = ClientesBLL.GetList(e => e.NoCedula.Contains(CriterioTextBox.Text));
                        break;
                    case 2:
                        listado = ClientesBLL.GetList(e => e.Nombres.Contains(CriterioTextBox.Text));
                        break;
                    case 3:
                        listado = ClientesBLL.GetList(e => e.Apellidos.Contains(CriterioTextBox.Text));
                        break;
                    case 4:
                        listado = ClientesBLL.GetList(e => e.Direccion.Contains(CriterioTextBox.Text));
                        break;
                    case 5:
                        listado = ClientesBLL.GetList(e => e.Telefono.Contains(CriterioTextBox.Text));
                        break;
                }
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
