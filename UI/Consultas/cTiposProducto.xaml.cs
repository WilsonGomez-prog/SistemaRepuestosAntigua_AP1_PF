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
    /// Lógica de interacción para cTiposProducto.xaml
    /// </summary>
    public partial class cTiposProducto : Window
    {
        public cTiposProducto()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            List<TiposProducto> listado = new List<TiposProducto>();
            try
            {

                if (!string.IsNullOrEmpty(CriterioTextBox.Text))
                { 
                    if (FiltroComboBox.SelectedItem != null)
                    {
                        switch (FiltroComboBox.SelectedIndex)
                        {

                            case 0:
                                listado = TiposProductoBLL.GetList(p => p.TipoProductoId == int.Parse(CriterioTextBox.Text));
                                break;

                            case 1:
                                listado = TiposProductoBLL.GetList(p => p.Descripcion.Contains(CriterioTextBox.Text));
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
                    listado = TiposProductoBLL.GetList(c => true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;


        }
    }
}
