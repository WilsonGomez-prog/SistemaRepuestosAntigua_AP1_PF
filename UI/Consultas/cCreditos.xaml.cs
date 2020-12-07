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

        private List<dynamic> GetDisplay(List<Creditos> lista)
        {
            var listado = new List<dynamic>();

            foreach (var credito in lista)
            {
                var cli = ClientesBLL.Buscar(credito.ClienteId);
                var cred = new
                {
                    credito.CreditoId,
                    Cliente = cli.Nombres + " " + cli.Apellidos,
                    credito.Monto,
                    credito.Balance,
                    UsuarioModificador = credito.UsuarioModificador != 0 ? UsuariosBLL.Buscar(credito.UsuarioModificador).NombreUsuario : "Default"
                };

                listado.Add(cred);
            }

            return listado;
        }

        private List<Creditos> FiltrarValor(List<Creditos> lista, int op)

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
                        lista = lista.FindAll(c => c.Monto <= Convert.ToSingle(ValorMaxTextbox.Text));
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Monto >= Convert.ToSingle(ValorMinTextbox.Text));
                    }
                    else
                    {
                        lista = lista.FindAll(c => c.Monto >= Convert.ToSingle(ValorMinTextbox.Text) && c.Monto <= Convert.ToSingle(ValorMaxTextbox.Text));
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
                        lista = lista.FindAll(c => c.Balance <= Convert.ToSingle(ValorMaxTextbox.Text));
                    }
                    else if (string.IsNullOrWhiteSpace(ValorMaxTextbox.Text))
                    {
                        lista = lista.FindAll(c => c.Balance >= Convert.ToSingle(ValorMinTextbox.Text));
                    }
                    else
                    {
                        lista = lista.FindAll(c => c.Balance >= Convert.ToSingle(ValorMinTextbox.Text) && c.Balance <= Convert.ToSingle(ValorMaxTextbox.Text));
                    }
                    break;
            }
            return lista;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Creditos>();
            var list = new List<dynamic>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                if (ValorComboBox.SelectedItem == null && string.IsNullOrWhiteSpace(ValorMaxTextbox.Text) && string.IsNullOrWhiteSpace(ValorMinTextbox.Text))
                {
                    listado = CreditosBLL.GetList(e => true);
                    list = GetDisplay(listado);
                }
                else
                {
                    listado = CreditosBLL.GetList(e => true);

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
                                listado = CreditosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                list = GetDisplay(listado);
                                break;
                            case 1:
                                listado = CreditosBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                list = GetDisplay(listado);
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
                                        listado = CreditosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        listado = FiltrarValor(listado, 0);
                                        list = GetDisplay(listado);
                                        break;
                                    case 1:
                                        listado = CreditosBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        listado = FiltrarValor(listado, 0);
                                        list = GetDisplay(listado);
                                        break;
                                }
                                break;
                            case 1:
                                switch (FiltroComboBox.SelectedIndex)
                                {
                                    case 0:
                                        listado = CreditosBLL.GetList(e => e.CreditoId == Convert.ToInt32(CriterioTextBox.Text));
                                        listado = FiltrarValor(listado, 1);
                                        list = GetDisplay(listado);
                                        break;
                                    case 1:
                                        listado = CreditosBLL.GetList(e => e.ClienteId == Convert.ToInt32(CriterioTextBox.Text));
                                        listado = FiltrarValor(listado, 1);
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
