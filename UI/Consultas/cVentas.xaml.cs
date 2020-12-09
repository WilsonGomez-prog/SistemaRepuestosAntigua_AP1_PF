using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;

namespace SistemaRepuestosAntigua_AP1_PF.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cVentas.xaml
    /// </summary>
    public partial class cVentas : Window
    {
        public cVentas()
        {
            InitializeComponent();
            DesdeDatePicker.SelectedDate = Convert.ToDateTime("01/01/0001");
            HastaDatePicker.SelectedDate = DateTime.Now;
        }

        private List<dynamic> GetDisplay (List<Ventas> lista)
        {
            var listado = new List<dynamic>();

            foreach (var venta in lista)
            {
                var cliente = ClientesBLL.Buscar(venta.ClienteId);
                var ven = new
                {
                    venta.VentaId,
                    Cliente = cliente.Nombres + " " + cliente.Apellidos,
                    Tipo = venta.TipoVenta == 1 ? "Crédito" : "Contado",
                    venta.Ncf,
                    Fecha = venta.Fecha.ToString("dd/MM/yyy"),
                    FechaVencimiento = venta.FechaVencimiento.ToString("dd/MM/yyy"),
                    Itbis = venta.Itbis.ToString("N2"),
                    Total = venta.Total.ToString("N2"),
                    PendientePagar = venta.PendientePagar.ToString("N2"),
                    UsuarioModificador = venta.UsuarioModificador != 0 ? UsuariosBLL.Buscar(venta.UsuarioModificador).NombreUsuario : "Default"
                };

                listado.Add(ven);
            }

            return listado;
        }

        private List<Ventas> FiltrarFecha(List<Ventas> lista, ComboBox fecha)
        {
            if(fecha.SelectedItem != null)
            {
                switch (fecha.SelectedIndex)
                {
                    case 0:
                        lista = lista.FindAll(c => c.Fecha >= DesdeDatePicker.SelectedDate && c.Fecha <= HastaDatePicker.SelectedDate);
                        break;
                    case 1:
                        lista = lista.FindAll(c => c.FechaVencimiento >= DesdeDatePicker.SelectedDate && c.FechaVencimiento <= HastaDatePicker.SelectedDate);
                        break;
                }
            }
            return lista;
        }

        private List<Ventas> FiltrarValor(List<Ventas> lista, int op)
        {
            switch (op)
            {
                case 0:
                    if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        MessageBox.Show("Debe de debe de introducir un valor mínimo o un máximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                        ValorMinTextbox.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Itbis <= Convert.ToSingle(ValorMaxTextbox.Text));
                        FiltrarFecha(lista, FechaComboBox);
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Itbis >= Convert.ToSingle(ValorMinTextbox.Text));
                        FiltrarFecha(lista, FechaComboBox);
                    }
                    else
                    {
                        lista = lista.FindAll(c => (float)c.Itbis >= Convert.ToSingle(ValorMinTextbox.Text) && (float)c.Itbis <= Convert.ToSingle(ValorMaxTextbox.Text));
                        FiltrarFecha(lista, FechaComboBox);
                    }
                    break;
                case 1:
                    if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        MessageBox.Show("Debe de debe de introducir un valor mínimo o un máximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                        ValorMinTextbox.Focus();
                    }    
                    else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Total <= Convert.ToSingle(ValorMaxTextbox.Text));
                        FiltrarFecha(lista, FechaComboBox);
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Total >= Convert.ToSingle(ValorMinTextbox.Text));
                        FiltrarFecha(lista, FechaComboBox);
                    }
                    else
                    {
                        lista = lista.FindAll(c => c.Total >= Convert.ToSingle(ValorMinTextbox.Text) && c.Total <= Convert.ToSingle(ValorMaxTextbox.Text));
                        FiltrarFecha(lista, FechaComboBox);
                    }
                    break;
            }
            return lista;
        }


        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ventas>();
            var list = new List<dynamic>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                if (ValorComboBox.SelectedItem == null)
                {
                    listado = FiltrarFecha(VentasBLL.GetList(e => true), FechaComboBox);
                    list = GetDisplay(listado);
                }
                else
                {
                    listado = FiltrarFecha(VentasBLL.GetList(e => true), FechaComboBox);
                    switch (ValorComboBox.SelectedIndex)
                    {
                        case 0:
                            listado = FiltrarValor(listado, 0);
                            list = GetDisplay(listado);
                            break;
                        case 1:
                            listado = FiltrarValor(listado, 1);
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
                                listado = FiltrarFecha(VentasBLL.GetList(e => e.VentaId == Convert.ToInt32(CriterioTextBox.Text)),FechaComboBox);
                                list = GetDisplay(listado);
                                break;
                            case 1:
                                listado = FiltrarFecha(VentasBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text)), FechaComboBox);
                                list = GetDisplay(listado);
                                break;
                            case 2:
                                listado = FiltrarFecha(VentasBLL.GetList(e => e.Ncf.Contains(CriterioTextBox.Text)), FechaComboBox);
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
                                        listado = FiltrarValor(VentasBLL.GetList(e => e.VentaId == Convert.ToInt32(CriterioTextBox.Text)), 0);
                                        list = GetDisplay(listado);
                                        break;
                                    case 1:
                                        listado = FiltrarValor(VentasBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text)), 0);
                                        list = GetDisplay(listado);
                                        break;
                                    case 2:
                                        listado = FiltrarValor(VentasBLL.GetList(e => e.Ncf.Contains(CriterioTextBox.Text)), 0);
                                        list = GetDisplay(listado);
                                        break;
                                }
                                break;
                            case 1:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        listado = FiltrarValor(VentasBLL.GetList(e => e.VentaId == Convert.ToInt32(CriterioTextBox.Text)), 1);
                                        list = GetDisplay(listado);
                                        break;
                                    case 1:
                                        listado = FiltrarValor(VentasBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text)), 1);
                                        list = GetDisplay(listado);
                                        break;
                                    case 2:
                                        listado = FiltrarValor(VentasBLL.GetList(e => e.Ncf.Contains(CriterioTextBox.Text)), 1);
                                        list = GetDisplay(listado);
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

