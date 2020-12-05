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
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                if (ValorComboBox.SelectedItem == null)
                {
                    listado = CobrosBLL.GetList(e => true);
                    FiltrarFecha(ref listado, FechaComboBox);
                }
                else
                {
                    switch (ValorComboBox.SelectedIndex)
                    {
                        case 0:
                            listado = FiltrarValor(listado, 0);
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
                                break;
                            case 1:
                                listado = CobrosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                FiltrarFecha(ref listado, FechaComboBox);
                                break;
                            case 2:
                                listado = CobrosBLL.GetList(e => e.EmpleadoId == Convert.ToInt32(CriterioTextBox.Text));
                                FiltrarFecha(ref listado, FechaComboBox);
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
                                        break;
                                    case 1:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text)), 0);
                                        break;
                                    case 2:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.EmpleadoId == Convert.ToInt32(CriterioTextBox.Text)), 0);
                                        break;
                                }
                                break;
                            case 1:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.CobroId == Convert.ToInt32(CriterioTextBox.Text)), 1);
                                        break;
                                    case 1:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text)), 1);
                                        break;
                                    case 2:
                                        listado = FiltrarValor(CobrosBLL.GetList(e => e.EmpleadoId == Convert.ToInt32(CriterioTextBox.Text)), 1);
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
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
