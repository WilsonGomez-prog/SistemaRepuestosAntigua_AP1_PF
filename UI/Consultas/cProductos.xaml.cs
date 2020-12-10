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
    /// Interaction logic for cProductos.xaml
    /// </summary>
    public partial class cProductos : Window
    {
        public cProductos()
        {
            InitializeComponent();
        }

        private List<dynamic> GetDisplay(List<Productos> lista)
        {
            var listado = new List<dynamic>();

            foreach (var producto in lista)
            {
                var prod = new
                {
                    producto.ProductoId,
                    producto.Codigo,
                    producto.Descripcion,
                    PrecioUnit = producto.PrecioUnit.ToString("N2"),
                    Tipo = TiposProductoBLL.Buscar(producto.TipoProductoId).Descripcion,
                    producto.Descuento,
                    producto.Existencia,
                    UsuarioModificador = producto.UsuarioModificador != 0 ? UsuariosBLL.Buscar(producto.UsuarioModificador).NombreUsuario : "Default"
                };

                listado.Add(prod);
            }

            return listado;
        }

        private List<Productos> FiltrarValor(List<Productos> lista, int op)
        {
            switch (op)
            {
                case 0:
                    if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                        ValorMinTextbox.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.PrecioUnit <= Convert.ToSingle(ValorMaxTextbox.Text));
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.PrecioUnit >= Convert.ToSingle(ValorMinTextbox.Text));
                    }
                    else
                    {
                        lista = lista.FindAll(c => (float)c.PrecioUnit >= Convert.ToSingle(ValorMinTextbox.Text) && (float)c.PrecioUnit <= Convert.ToSingle(ValorMaxTextbox.Text));
                    }
                    break;
                case 1:
                    if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                        ValorMinTextbox.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Existencia <= Convert.ToInt32(ValorMaxTextbox.Text));
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Existencia >= Convert.ToInt32(ValorMinTextbox.Text));
                    }
                    else
                    {
                        lista = lista.FindAll(c => c.Existencia >= Convert.ToInt32(ValorMinTextbox.Text) && c.Existencia <= Convert.ToInt32(ValorMaxTextbox.Text));
                    }
                    break;
            }
            return lista;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Productos>();
            List<dynamic> list = new List<dynamic>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                if (ValorComboBox.SelectedItem == null)
                {
                    listado = ProductosBLL.GetList(e => true);
                    list = GetDisplay(listado);
                }
                else
                {
                    switch (ValorComboBox.SelectedIndex)
                    {
                        case 0:
                            listado = FiltrarValor(ProductosBLL.GetList(e => true), 0);
                            list = GetDisplay(listado);
                            break;
                        case 1:
                            listado = FiltrarValor(ProductosBLL.GetList(e => true), 1);
                            list = GetDisplay(listado);
                            break;
                    }
                }
            }
            else
            {
                if (FiltroComboBox.SelectedItem != null)
                {
                    if (ValorComboBox.SelectedItem == null)
                    {
                        switch (FiltroComboBox.SelectedIndex)
                        {
                            case 0:
                                listado = ProductosBLL.GetList(e => e.ProductoId == Convert.ToInt32(CriterioTextBox.Text));
                                list = GetDisplay(listado);
                                break;
                            case 1:
                                listado = ProductosBLL.GetList(e => e.Descripcion.Contains(CriterioTextBox.Text));
                                list = GetDisplay(listado);
                                break;
                        }
                    }
                    else
                    {
                        switch (ValorComboBox.SelectedIndex)
                        {
                            case 0:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        listado = ProductosBLL.GetList(e => e.ProductoId == Convert.ToInt32(CriterioTextBox.Text));
                                        list = GetDisplay(listado);
                                        listado = FiltrarValor(listado, 0);
                                        break;
                                    case 1:
                                        listado = ProductosBLL.GetList(e => e.Descripcion.Contains(CriterioTextBox.Text));
                                        list = GetDisplay(listado);
                                        listado = FiltrarValor(listado, 0);
                                        break;
                                }
                                break;
                            case 1:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        listado = ProductosBLL.GetList(e => e.ProductoId == Convert.ToInt32(CriterioTextBox.Text));
                                        list = GetDisplay(listado);
                                        listado = FiltrarValor(listado, 1);
                                        break;
                                    case 1:
                                        listado = ProductosBLL.GetList(e => e.Descripcion.Contains(CriterioTextBox.Text));
                                        list = GetDisplay(listado);
                                        listado = FiltrarValor(listado, 1);
                                        break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar un filtro para poder evaluar un criterio de búsqueda.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    FiltroComboBox.Focus();
                }
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = list;
        }
    }
}
