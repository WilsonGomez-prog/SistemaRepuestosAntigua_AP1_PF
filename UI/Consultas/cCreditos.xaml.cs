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
    /// Interaction logic for cCreditos.xaml
    /// </summary>
    public partial class cCreditos : Window
    {
        public cCreditos()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Creditos>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                if (ValorComboBox.SelectedItem == null)
                {
                    listado = CreditosBLL.GetList(e => true);
                }
                else
                {
                    switch (ValorComboBox.SelectedIndex)
                    {
                        case 0:
                            if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                            {
                                listado = CreditosBLL.GetList(c => c.Monto >= Convert.ToDecimal(ValorMinTextbox.Text));
                            }
                            else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                            {
                                listado = CreditosBLL.GetList(c => c.Monto <= Convert.ToDecimal(ValorMaxTextbox.Text));
                            }
                            else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                            {
                                MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                                ValorMinTextbox.Focus();
                            }
                            else
                            {
                                listado = CreditosBLL.GetList(c => (float)c.Monto >= Convert.ToSingle(ValorMinTextbox.Text) && (float)c.Monto <= Convert.ToSingle(ValorMaxTextbox.Text));
                            }
                            break;
                        case 1:
                            if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                            {
                                listado = CreditosBLL.GetList(c => c.Balance >= Convert.ToDecimal(ValorMinTextbox.Text));
                            }
                            else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                            {
                                listado = CreditosBLL.GetList(c => c.Balance <= Convert.ToDecimal(ValorMaxTextbox.Text));
                            }
                            else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                            {
                                MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                                ValorMinTextbox.Focus();
                            }
                            else
                            {
                                listado = CreditosBLL.GetList(c => c.Balance >= Convert.ToDecimal(ValorMinTextbox.Text) && c.Balance <= Convert.ToDecimal(ValorMaxTextbox.Text));
                            }
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
                                listado = CreditosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                break;
                            case 1:
                                listado = CreditosBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                break;
                        }
                    }
                    else
                    {
                        switch(ValorComboBox.SelectedIndex)
                        {
                            case 0:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Monto >= Convert.ToDecimal(ValorMinTextbox.Text)).FindAll(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Monto <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                                            ValorMinTextbox.Focus();
                                        }
                                        else
                                        {
                                            listado = CreditosBLL.GetList(c => c.Monto >= Convert.ToDecimal(ValorMinTextbox.Text) && c.Monto <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        break;
                                    case 1:
                                        if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Monto >= Convert.ToDecimal(ValorMinTextbox.Text)).FindAll(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Monto <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                                            ValorMinTextbox.Focus();
                                        }
                                        else
                                        {
                                            listado = CreditosBLL.GetList(c => c.Monto >= Convert.ToDecimal(ValorMinTextbox.Text) && c.Monto <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        break;
                                }
                                break;
                            case 1:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Balance >= Convert.ToDecimal(ValorMinTextbox.Text)).FindAll(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Balance <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                                            ValorMinTextbox.Focus();
                                        }
                                        else
                                        {
                                            listado = CreditosBLL.GetList(c => c.Balance >= Convert.ToDecimal(ValorMinTextbox) && c.Balance <= Convert.ToDecimal(ValorMaxTextbox)).FindAll(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        break;
                                    case 1:
                                        if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Balance >= Convert.ToDecimal(ValorMinTextbox.Text)).FindAll(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            listado = CreditosBLL.GetList(c => c.Balance <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
                                        else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                                        {
                                            MessageBox.Show("Debe de debe de introducir un valor minimo o un maximo para poder filtrar por algun tipo de valor.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                                            ValorMinTextbox.Focus();
                                        }
                                        else
                                        {
                                            listado = CreditosBLL.GetList(c => c.Balance >= Convert.ToDecimal(ValorMinTextbox.Text) && c.Balance <= Convert.ToDecimal(ValorMaxTextbox.Text)).FindAll(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        }
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
