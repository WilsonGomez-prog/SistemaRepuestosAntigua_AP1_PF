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
        Usuarios Modificador;
        List<dynamic> detalle;
        public rCobros(Usuarios usuario)
        {
            InitializeComponent();
            cobros = new Cobros();
            this.DataContext = cobros;
            detalle = new List<dynamic>();
            Modificador = usuario;
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
                                    Display = ClientesBLL.Buscar(ven.ClienteId).Nombres +" "+ ClientesBLL.Buscar(ven.ClienteId).Apellidos + " - " + ven.Fecha.ToString("dd-MM-yyyy")
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
            detalle = new List<dynamic>();
            DetalleDataGrid.ItemsSource = detalle;
        }

        public bool Existe()
        {
            var proyecto = CobrosBLL.Buscar(Convert.ToInt32(CobroIdTextBox.Text));

            return proyecto != null;
        }

        public void Actualizar(int op, int del)
        {
            Contexto context = new Contexto();

            if (op == 1)
            {
                detalle = new List<dynamic>();
                foreach (CobrosDetalle cobro in cobros.DetalleCobro)
                {
                    var ven = VentasBLL.Buscar(cobro.VentaId);
                    var det = new { cobro.CobroDetalleId, ven.VentaId, cobro.Fecha, cobro.Monto, ven.PendientePagar};
                    this.detalle.Add(det);
                }
            }
            else
            {
                this.detalle.RemoveAt(del);
            }

            this.DataContext = cobros;
            CreditoIdCombobox.SelectedIndex = cobros.CreditoId - 1;
            TotalTextbox.Text = cobros.Total.ToString();
            DetalleDataGrid.ItemsSource = detalle;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CobroIdTextBox.Text) || !Char.IsDigit(CobroIdTextBox.Text[0]))
            {
                var cobro = CobrosBLL.Buscar(Convert.ToInt32(CobroIdTextBox.Text));

                if (cobro != null)
                {
                    this.cobros = cobro;
                    this.DataContext = cobros;
                    CreditoIdCombobox.SelectedIndex = cobros.CreditoId - 1;
                    Actualizar(1, 0);
                    MessageBox.Show("El cobro que ha buscado ha sido encontrado!!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("El cobro que ha buscado no ha sido encontrado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("El Cobro ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CobroIdTextBox.Focus();
            }

            this.DataContext = cobros;
        }

        private bool ValidarCobroVenta()
        {
            bool valido = true;


            if (CobrosBLL.Buscar(cobros.CobroId) == null)
            {
                valido = false;
                MessageBox.Show("Debe de guardar el cobro para agregar el detalle.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                GuardarButton.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaMonetaria(MontoTextbox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla monto no puede tener letras ni caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                MontoTextbox.Focus();
            }
            else if (VentaIdCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar una venta para hacerle el cobro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                VentaIdCombobox.Focus();
            }
            else if (FechaCobroDatePicker.SelectedDate > DateTime.Now)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar una fecha menor o igual a la actual para cobrar la venta.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                FechaCobroDatePicker.Focus();
            }
            else if (Convert.ToSingle(MontoTextbox.Text) > VentasBLL.Buscar(Convert.ToInt32(VentaIdCombobox.SelectedValue)).PendientePagar)
            {
                valido = false;
                MessageBox.Show("Debe de ingresar un monto menor o igual al monto pendiente de la venta a cobrar.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                FechaCobroDatePicker.Focus();
            }

            return valido;
        }

        private bool ValidarCobro()
        {
            bool valido = true;

            if (CreditoIdCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un crédito para asignar al cobro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CreditoIdCombobox.Focus();
            }
            else if (FechaDatePicker.SelectedDate > DateTime.Now)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar una fecha menor o igual a la actual para registrar el cobro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                FechaDatePicker.Focus();
            }

            return valido;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un cobro nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Limpiar();
            }            
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el cobro?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (ValidarCobro())
                {
                    bool guardado;
                    cobros.UsuarioModificador = Modificador.UsuarioId;
                    cobros.EmpleadoId = EmpleadosBLL.GetList(e => e.UsuarioId == Modificador.UsuarioId).FirstOrDefault().EmpleadoId;
                    cobros.CreditoId = Convert.ToInt32(CreditoIdCombobox.SelectedValue);
                    cobros.Fecha = Convert.ToDateTime(FechaDatePicker.SelectedDate.Value.Date.ToShortDateString());

                    if (string.IsNullOrWhiteSpace(CobroIdTextBox.Text) || CobroIdTextBox.Text == "0")
                        guardado = CobrosBLL.Guardar(cobros);
                    else
                    {
                        if (!Existe())
                        {
                            MessageBox.Show("Este cobro no se puede modificar \nporque no existe en la base de datos.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        guardado = CobrosBLL.Modificar(cobros);
                    }

                    if (guardado)
                    {
                        Limpiar();
                        MessageBox.Show("El cobro ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El cobro no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el cobro?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
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
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCobroVenta())
            {
                CobrosDetalle ven = new CobrosDetalle(Convert.ToInt32(CobroIdTextBox.Text), Convert.ToInt32(VentaIdCombobox.SelectedValue), (DateTime)FechaCobroDatePicker.SelectedDate, Convert.ToSingle(MontoTextbox.Text));
                this.cobros.DetalleCobro.Add(ven);
                cobros.Total = 0;
                foreach (CobrosDetalle detalle in cobros.DetalleCobro)
                {
                    cobros.Total += detalle.Monto;
                }
                Actualizar(1, 0);
            }
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            int del = DetalleDataGrid.FrozenColumnCount;
            cobros.DetalleCobro.RemoveAt(del);
            cobros.Total = 0;
            foreach (CobrosDetalle detalle in cobros.DetalleCobro)
            {
                cobros.Total += detalle.Monto;
            }
            Actualizar(0, del);
        }
    }
}
