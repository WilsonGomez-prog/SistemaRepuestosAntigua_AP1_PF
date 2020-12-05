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
using BLL;

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rVentas.xaml
    /// </summary>
    public partial class rVentas : Window
    {
        Ventas Venta;
        Usuarios Modificador;
        List<dynamic> detalle;
        public rVentas(Usuarios usuario)
        {
            InitializeComponent();
            Venta = new Ventas();
            this.DataContext = Venta;
            Modificador = usuario;
            detalle = new List<dynamic>();
            Contexto context = new Contexto();

            var cre = new { TipoId = 1, Descripcion = "Crédito" };
            var con = new { TipoId = 0, Descripcion = "Contado" };

            var clientes = (from cli in context.Clientes select new { cli.ClienteId, Display = cli.Nombres + " " + cli.Apellidos + " - " + cli.NoCedula}).ToList();

            TipoVentaCombobox.ItemsSource = new List<dynamic>(){con, cre};
            TipoVentaCombobox.SelectedValuePath = "TipoId";
            TipoVentaCombobox.DisplayMemberPath = "Descripcion";

            ClienteIdCombobox.ItemsSource = clientes;
            ClienteIdCombobox.SelectedValuePath = "ClienteId";
            ClienteIdCombobox.DisplayMemberPath = "Display";

            ProductoCombobox.ItemsSource = ProductosBLL.GetList(p => true);
            ProductoCombobox.SelectedValuePath = "ProductoId";
            ProductoCombobox.DisplayMemberPath = "Descripcion";
        }

        private void Limpiar()
        {
            Venta = new Ventas();
            this.DataContext = Venta;
            detalle = new List<dynamic>();
            ProductoCombobox.SelectedIndex = -1;
            CantidadTextBox.Text = string.Empty;
            SubTotalTextbox.Text = string.Empty;
            DetalleDataGrid.ItemsSource = detalle;
        }

        public bool Existe()
        {
            var proyecto = VentasBLL.Buscar(Convert.ToInt32(VentaIdTextBox.Text));

            return proyecto != null;
        }

        private void Actualizar(int op, int del)
        {
            Contexto contexto = new Contexto();

            if (op == 1)
            {
                foreach (VentasDetalle detalle in Venta.DetalleVenta)
                {
                    var prod = ProductosBLL.Buscar(detalle.ProductoId);
                    var det = new { detalle.DetalleVentaId, prod.Codigo, Producto = prod.Descripcion, detalle.Cantidad, prod.PrecioUnit, detalle.Total};
                    this.detalle.Add(det);
                }
            }
            else
            {
                this.detalle.RemoveAt(del);
            }

            this.DataContext = null;
            this.DataContext = Venta;
            TipoVentaCombobox.SelectedIndex = Venta.TipoVenta - 1;
            ClienteIdCombobox.SelectedIndex = Venta.ClienteId - 1;
            FechaDatePicker.SelectedDate = Venta.Fecha;
            VencimientoDatePicker.SelectedDate = Venta.FechaVencimiento;
            ProductoCombobox.SelectedIndex = -1;
            CantidadTextBox.Text = string.Empty;
            DetalleDataGrid.ItemsSource = this.detalle;
            SubTotalTextbox.Text = Convert.ToString(Venta.Total - Venta.Itbis);

        }

        private bool ValidarProd()
        {
            bool valido = true;

            if (!Utilidades.Utilidades.ValidarCasillaNumerica(CantidadTextBox.Text))
            {
                MessageBox.Show("La casilla cantidad no puede tener letras ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadTextBox.Focus();
            }
            else if (ProductoCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un producto para agregar a la venta.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductoCombobox.Focus();
            }

            return valido;
        }

        private bool ValidarVenta()
        {
            bool valido = true;

            if (!Char.IsDigit((char)VentaIdTextBox.Text[0]))
            {
                MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                VentaIdTextBox.Focus();
            }
            else if (ClienteIdCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un cliente para asignar la venta.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClienteIdCombobox.Focus();
            }
            else if (TipoVentaCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de especificar si la venta es al contado o a crédito.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                TipoVentaCombobox.Focus();
            }

            return valido;
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarProd())
            {
                var prod = ProductosBLL.Buscar(Convert.ToInt32(ProductoCombobox.SelectedValue));
                float totalProd = (prod.PrecioUnit - (prod.PrecioUnit * prod.Descuento / 100)) + (prod.PrecioUnit * (prod.Impuesto / 100));

                VentasDetalle ven = new VentasDetalle(Venta.VentaId, Convert.ToInt32(ProductoCombobox.SelectedValue), Convert.ToInt32(CantidadTextBox.Text), totalProd * Convert.ToInt32(CantidadTextBox.Text));
                
                this.Venta.DetalleVenta.Add(ven);
                Venta.Itbis = 0;
                Venta.Total = 0;
                foreach (VentasDetalle detalle in Venta.DetalleVenta)
                {
                    var produ = ProductosBLL.Buscar(detalle.ProductoId);
                    float bruto = produ.PrecioUnit-(produ.PrecioUnit * produ.Descuento/100);
                    Venta.Itbis += produ.PrecioUnit * detalle.Cantidad * (produ.Impuesto / 100);
                    Venta.Total += detalle.Total;
                }
                Actualizar(1, 0);
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(VentaIdTextBox.Text) || !Char.IsDigit((char)VentaIdTextBox.Text[0]))
            {
                var venta = VentasBLL.Buscar(Convert.ToInt32(VentaIdTextBox.Text));

                if (venta != null)
                {
                    this.Venta = venta;
                    this.DataContext = Venta;
                    ClienteIdCombobox.SelectedIndex = Venta.ClienteId - 1;
                    TipoVentaCombobox.SelectedIndex = Venta.TipoVenta;
                    Actualizar(1, 0);
                    MessageBox.Show("La venta que ha buscado ha sido encontrada!!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("La venta que ha buscado no ha sido encontrada.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                VentaIdTextBox.Focus();
            }
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            int ve = DetalleDataGrid.FrozenColumnCount;
            this.Venta.DetalleVenta.RemoveAt(ve);
            Venta.Itbis = 0;
            Venta.Total = 0;
            foreach (VentasDetalle detalle in Venta.DetalleVenta)
            {
                var produ = ProductosBLL.Buscar(detalle.ProductoId);
                float bruto = produ.PrecioUnit - (produ.PrecioUnit * (produ.PrecioUnit / produ.Descuento));
                Venta.Itbis += (bruto + produ.PrecioUnit * (produ.Impuesto/100)) - bruto;
                Venta.Total += detalle.Total;
            }
            Actualizar(0, ve);
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar una venta nueva? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar la venta?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (ValidarVenta())
                {
                    bool guardado;
                    Venta.Fecha = Convert.ToDateTime(FechaDatePicker.SelectedDate.Value.Date.ToShortDateString());
                    Venta.FechaVencimiento = Convert.ToDateTime(VencimientoDatePicker.SelectedDate.Value.Date.ToShortDateString());
                    Venta.TipoVenta = TipoVentaCombobox.SelectedIndex;
                    Venta.ClienteId = Convert.ToInt32(ClienteIdCombobox.SelectedValue);
                    Venta.UsuarioModificador = Modificador.UsuarioId;
                    if(Venta.TipoVenta == 1)
                    {
                        Venta.PendientePagar = Venta.Total;
                    }

                    if (string.IsNullOrWhiteSpace(VentaIdTextBox.Text) || VentaIdTextBox.Text == "0")
                        guardado = VentasBLL.Guardar(Venta);
                    else
                    {
                        if (!Existe())
                        {
                            MessageBox.Show("Esta venta no se puede modificar \nporque no existe en la base de datos.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        guardado = VentasBLL.Modificar(Venta);
                    }

                    if (guardado)
                    {
                        Limpiar();
                        MessageBox.Show("La venta ha sido guardada correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("La venta no ha podido ser guardada.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar la venta?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(VentaIdTextBox.Text) || !Char.IsDigit((char)VentaIdTextBox.Text[0]))
                {
                    if (CreditosBLL.Eliminar(Convert.ToInt32(VentaIdTextBox.Text)))
                    {
                        MessageBox.Show("La venta ha sido eliminada correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("La venta no pudo ser eliminada.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    VentaIdTextBox.Focus();
                }
            }
        }
    }
}
