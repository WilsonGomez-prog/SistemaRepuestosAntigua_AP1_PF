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

        private List<dynamic> GetDisplay(List<Clientes> lista)
        {
            var listado = new List<dynamic>();

            foreach (var clientes in lista)
            {
                var cli = new
                {
                    clientes.ClienteId,
                    clientes.Nombres,
                    clientes.Apellidos,
                    clientes.Telefono,
                    clientes.NoCedula,
                    clientes.Rnc,
                    clientes.Direccion,
                    UsuarioModificador = clientes.UsuarioModificador != 0 ? UsuariosBLL.Buscar(clientes.UsuarioModificador).NombreUsuario : "Default"
                };

                listado.Add(cli);
            }

            return listado;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Clientes>(); 
            var list = new List<dynamic>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                listado = ClientesBLL.GetList(e => true);
                list = GetDisplay(listado);
            }
            else
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = ClientesBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                    case 1:
                        listado = ClientesBLL.GetList(e => e.NoCedula.Contains(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                    case 2:
                        listado = ClientesBLL.GetList(e => e.Nombres.Contains(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                    case 3:
                        listado = ClientesBLL.GetList(e => e.Apellidos.Contains(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                    case 4:
                        listado = ClientesBLL.GetList(e => e.Direccion.Contains(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                    case 5:
                        listado = ClientesBLL.GetList(e => e.Telefono.Contains(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                }
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = list;
        }

    }
}
