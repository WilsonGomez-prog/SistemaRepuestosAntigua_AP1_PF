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
    public partial class cCobros : Window
    {
        public cCobros()
        {
            InitializeComponent();
        }

        private List<dynamic> GetDisplay(List<Cobros> lista)
        {
            var listado = new List<dynamic>();

            foreach (var cobro in lista)
            {
                var cob = new
                {
                    cobro.CobroId,
                    Fecha = cobro.Fecha.ToString("dd/MM/yyy"),
                    Total = cobro.Total.ToString("N2"),
                    Cliente = ClientesBLL.Buscar(cobro.ClienteId).Nombres + " " + ClientesBLL.Buscar(cobro.ClienteId).Apellidos,
                    UsuarioModificador = cobro.UsuarioModificador != 0 ? UsuariosBLL.Buscar(cobro.UsuarioModificador).NombreUsuario : "Default"
                };

                listado.Add(cob);
            }

            return listado;
        }

        private void FiltrarFecha(ref List<Cobros> lista, ComboBox fecha)
        {
            if (fecha.SelectedItem != null)
            {
                switch (fecha.SelectedIndex)
                {
                    case 0:
                        lista = lista.FindAll(c => c.Fecha >= DesdeDatePicker.SelectedDate && c.Fecha <= HastaDatePicker.SelectedDate);
                        break;
                }
            }
        }

        private List<Cobros> FiltrarValor(List<Cobros> lista, int op)
        {
            switch (op)
            {
               
                case 0:
                    if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        MessageBox.Show("Debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                        ValorMinTextbox.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Total <= Convert.ToSingle(ValorMaxTextbox.Text));
                        FiltrarFecha(ref lista, FechaComboBox);
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Total >= Convert.ToSingle(ValorMinTextbox.Text));
                        FiltrarFecha(ref lista, FechaComboBox);
                    }
                    else
                    {
                        lista = lista.FindAll(c => c.Total >= Convert.ToSingle(ValorMinTextbox.Text) && c.Total <= Convert.ToSingle(ValorMaxTextbox.Text));
                        FiltrarFecha(ref lista, FechaComboBox);
                    }
                    break;
            }
            return lista;
        }


        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Cobros>();
            var list = new List<dynamic>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                if (ValorComboBox.SelectedItem == null)
                {
                    listado = CobrosBLL.GetList(e => true);
                    FiltrarFecha(ref listado, FechaComboBox);
                    list = GetDisplay(listado);
                }
                else
                {
                    switch (ValorComboBox.SelectedIndex)
                    {
                        case 0:
                            listado = FiltrarValor(listado, 0);
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
                                listado = CobrosBLL.GetList(e => e.CobroId == Convert.ToInt32(CriterioTextBox.Text));
                                FiltrarFecha(ref listado, FechaComboBox);
                                list = GetDisplay(listado);
                                break;
                            case 1:
                                listado = CobrosBLL.GetList(e => e.UsuarioModificador == Convert.ToInt32(CriterioTextBox.Text));
                                FiltrarFecha(ref listado, FechaComboBox);
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
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.CobroId == Convert.ToInt32(CriterioTextBox.Text)), 0);
                                        list = GetDisplay(listado);
                                        break;
                                    case 1:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.UsuarioModificador == Convert.ToInt32(CriterioTextBox.Text)), 0);
                                        list = GetDisplay(listado);
                                        break;
                                }
                                break;
                            case 1:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.CobroId == Convert.ToInt32(CriterioTextBox.Text)), 1);
                                        list = GetDisplay(listado);
                                        break;
                                    case 1:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.UsuarioModificador == Convert.ToInt32(CriterioTextBox.Text)), 1);
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
