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

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    public partial class rCobros : Window
    {
        Cobros cobros;
        
        public rCobros()
        {
            InitializeComponent();
            this.DataContext = cobros;

            DetalleDataGrid.ItemsSource = new List<CobrosDetalle>();
            Contexto context = new Contexto();

            var clientesCred = (from cli in context.Clientes
                                join cred in context.Creditos
                                on cli.ClienteId equals cred.ClienteId
                                select new
                                {
                                    cred.CreditoId,
                                    Cliente = cli.Nombres + " " + cli.Apellidos
                                }).ToList();

            var ventasCred = (from ven in context.Ventas
                                join cred in context.Creditos
                                on ven.ClienteId equals cred.ClienteId
                                where ven.TipoVenta == 1
                                select new
                                {
                                    ven.VentaId,
                                    Diplay = ClientesBLL.Buscar(ven.ClienteId).Nombres +" "+ ClientesBLL.Buscar(ven.ClienteId).Apellidos + " - " + ven.Fecha.ToString("dd-MM-yyyy")
                                }).ToList();

            CreditoIdCombobox.ItemsSource = clientesCred;
            CreditoIdCombobox.SelectedValuePath = "CreditoId";
            CreditoIdCombobox.DisplayMemberPath = "Cliente";

            VentaIdCombobox.ItemsSource = ventasCred;
            VentaIdCombobox.SelectedValuePath = "VentaId";
            VentaIdCombobox.DisplayMemberPath = "Display";

        }

        private void Limpiar()
        {
            this.cobros = new Cobros();
            this.DataContext = cobros;
        }

        public void Actualizar()
        {
            this.cobros = new Cobros();
            this.DataContext = cobros;
            DetalleDataGrid.ItemsSource = cobros.DetalleCobro;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CobroIdTextBox.Text) || !Char.IsDigit(CobroIdTextBox.Text[0]))
            {
                var cobros = CobrosBLL.Buscar(Convert.ToInt32(CobroIdTextBox.Text));

                if (cobros != null)
                {
                    this.cobros = cobros;
                }
                else
                {
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("El Cobro ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CobroIdTextBox.Focus();
            }

            this.DataContext = cobros;
        }

        private bool Validar()
        {
            bool valido = true;

            if (!ValidarCasillaTexto(CobroIdTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El Cobro ID no puede contener numeros o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CobroIdTextBox.Focus();
            }

            return valido;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                bool guardo = CobrosBLL.Guardar(cobros);

                if (guardo)
                {
                    Limpiar();
                    MessageBox.Show("El Cobro ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("El Cobro no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CobroIdTextBox.Text) || !Char.IsDigit(CobroIdTextBox.Text[0]))
            {
                if (CobrosBLL.Eliminar(Convert.ToInt32(CobroIdTextBox.Text)))
                {
                    MessageBox.Show("El Cobro ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("El Cobro no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("El Cobro ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CobroIdTextBox.Focus();
            }
        }



        private bool ValidarCasillaTexto(string texto)
        {

            foreach (char carac in texto.ToCharArray())
            {
                if (!Char.IsLetter(carac))
                {
                    return false;
                }
            }

            return true;
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            this.cobros.DetalleCobro.Add(new CobrosDetalle(Convert.ToInt32(CobroIdTextBox.Text), Convert.ToInt32(VentaIdCombobox.SelectedValue), Convert.ToDateTime(FechaCobroDatePicker.SelectedDate.ToString()), Convert.ToDecimal(MontoTextbox.Text)));
            cobros.Total = 0;
            foreach (CobrosDetalle detalle in cobros.DetalleCobro)
            {
                cobros.Total += detalle.Monto;
            }
            Actualizar();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            cobros.DetalleCobro.RemoveAt(DetalleDataGrid.FrozenColumnCount);
            cobros.Total = 0;
            foreach (CobrosDetalle detalle in cobros.DetalleCobro)
            {
                cobros.Total += detalle.Monto;
            }
            Actualizar();
        }
    }
}
