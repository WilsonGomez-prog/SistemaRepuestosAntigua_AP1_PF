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
                                join ven in context.Ventas
                                on cli.ClienteId equals ven.ClienteId
                                where ven.TipoVenta == 1
                                select new 
                                {
                                    cli.ClienteId,
                                    Cliente = cli.Nombres + " " + cli.Apellidos + " - " + cli.NoCedula
                                }).Distinct().ToList();

            ClienteCombobox.ItemsSource = clientesCred;
            ClienteCombobox.SelectedValuePath = "ClienteId";
            ClienteCombobox.DisplayMemberPath = "Cliente";

            VentaIdCombobox.ItemsSource = new List<dynamic>() {"Seleccione un cliente para seleccionar una venta."};

            PendienteTextbox.Text = "N/A";
        }

        

        private void Limpiar()
        {
            this.cobros = new Cobros();
            this.DataContext = cobros;
            detalle = new List<dynamic>();
            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = detalle;
            ClienteCombobox.SelectedIndex = -1;
            VentaIdCombobox.SelectedIndex = -1;
            MontoTextbox.Text = string.Empty;
            PendienteTextbox.Text = "N/A";
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
                    var det = new {cobro.CobroDetalleId, ven.VentaId, Fecha = cobro.Fecha.ToString("dd/MM/yyyy"), Monto = cobro.Monto.ToString("N2")};
                    this.detalle.Add(det);
                }
            }
            else
            {
                this.detalle.RemoveAt(del);
            }

            this.DataContext = cobros;
            TotalTextbox.Text = cobros.Total.ToString();
            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = detalle;
            VentaIdCombobox.SelectedIndex = -1;
            MontoTextbox.Text = string.Empty;
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
                    Actualizar(1, 0);
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

            if (!Utilidades.Utilidades.ValidarCasillaDecimal(MontoTextbox.Text) || string.IsNullOrWhiteSpace(MontoTextbox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla monto no puede tener letras ni caracteres especiales. o estar vacío", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                MontoTextbox.Focus();
            }
            else if (VentaIdCombobox.SelectedItem == null || VentaIdCombobox.SelectedIndex == -1)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar una venta para hacerle el cobro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                VentaIdCombobox.Focus();
            }
            else if (VentasBLL.Buscar(Convert.ToInt32(VentaIdCombobox.SelectedValue)).PendientePagar < Convert.ToSingle(MontoTextbox.Text))
            {
                valido = false;
                MessageBox.Show("Debe de ingresar un monto menor o igual al monto pendiente a pagar.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                MontoTextbox.Focus();
            }

            return valido;
        }

        private bool ValidarCobro()
        {
            bool valido = true;

            if (FechaDatePicker.SelectedDate > DateTime.Now)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar una fecha menor o igual a la actual para registrar el cobro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                FechaDatePicker.Focus();
            }
            else if (ClienteCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un cliente al cual le está registrando el cobro.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClienteCombobox.Focus();
            }
            else if (cobros.DetalleCobro.Count == 0)
            {
                valido = false;
                MessageBox.Show("No puede guardar un cobro que esté vacío.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return valido;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un cobro nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CobrosBLL.Buscar(Convert.ToInt32(CobroIdTextBox.Text)) != null)
                {
                    if (!CobrosBLL.Buscar(Convert.ToInt32(CobroIdTextBox.Text)).DetalleCobro.Equals(cobros.DetalleCobro))
                    {
                        MessageBox.Show("1");
                        foreach (var detalle in cobros.DetalleCobro)
                        {
                            var venta = VentasBLL.Buscar(detalle.VentaId);

                            venta.PendientePagar = venta.PendientePagar + detalle.Monto;

                            VentasBLL.Modificar(venta);
                        }
                        MessageBox.Show("2");
                        foreach (var detalleG in CobrosBLL.Buscar(Convert.ToInt32(CobroIdTextBox.Text)).DetalleCobro)
                        {
                            var venta = VentasBLL.Buscar(detalleG.VentaId);

                            venta.PendientePagar = venta.PendientePagar - detalleG.Monto;

                            VentasBLL.Modificar(venta);
                        }
                    }
                }
                else
                {
                    foreach (var detalle in cobros.DetalleCobro)
                    {
                        var venta = VentasBLL.Buscar(detalle.VentaId);
                        MessageBox.Show(venta.PendientePagar.ToString());
                        venta.PendientePagar = venta.PendientePagar + detalle.Monto;
                        MessageBox.Show(venta.PendientePagar.ToString());
                        VentasBLL.Modificar(venta);
                    }
                }

                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el cobro?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (ValidarCobro())
                {
                    bool guardado;
                    cobros.UsuarioModificador = Modificador.UsuarioId;
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
            if (MessageBox.Show("¿De verdad desea eliminar el cobro?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(CobroIdTextBox.Text) || !Char.IsDigit(CobroIdTextBox.Text[0]) || Convert.ToInt32(CobroIdTextBox.Text) == 0)
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
                CobrosDetalle cob = new CobrosDetalle(Convert.ToInt32(CobroIdTextBox.Text), Convert.ToInt32(VentaIdCombobox.SelectedValue), FechaDatePicker.SelectedDate.Value , Convert.ToSingle(MontoTextbox.Text));

                this.cobros.DetalleCobro.Add(cob);

                var venta = VentasBLL.Buscar(cob.VentaId);

                venta.PendientePagar = venta.PendientePagar - cob.Monto;

                VentasBLL.Modificar(venta);

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
            var cob = cobros.DetalleCobro.ElementAt(del);

            var venta = VentasBLL.Buscar(cob.VentaId);

            venta.PendientePagar = venta.PendientePagar + cob.Monto;

            VentasBLL.Modificar(venta);

            cobros.DetalleCobro.RemoveAt(del);
            cobros.Total = 0;

            foreach (CobrosDetalle detalle in cobros.DetalleCobro)
            {
                cobros.Total += detalle.Monto;
            }
            Actualizar(0, del);
        }

        private void VentaIdCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VentaIdCombobox.SelectedIndex != -1)
            {
                PendienteTextbox.Text = VentasBLL.Buscar(Convert.ToInt32(VentaIdCombobox.SelectedValue)).PendientePagar.ToString();
            }
            else
            {
                PendienteTextbox.Text = "N/A";
            }
        }

        private void ClienteCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool flag = true;
            if (ClienteCombobox.SelectedIndex != -1)
            {
                if (cobros.DetalleCobro.Count != 0)
                {
                    foreach (var detalle in cobros.DetalleCobro)
                    {
                        if (Convert.ToInt32(ClienteCombobox.SelectedValue) != VentasBLL.Buscar(detalle.VentaId).ClienteId)
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag)
                {
                    Contexto contexto = new Contexto();

                    var ventasCred = (from cli in contexto.Clientes
                                      join ven in contexto.Ventas
                                      on cli.ClienteId equals ven.ClienteId
                                      where ven.ClienteId == Convert.ToInt32(ClienteCombobox.SelectedValue)
                                      select new
                                      {
                                          ven.VentaId,
                                          Display = ven.VentaId
                                      }).ToList();

                    VentaIdCombobox.ItemsSource = ventasCred;
                    VentaIdCombobox.SelectedValuePath = "VentaId";
                    VentaIdCombobox.DisplayMemberPath = "Display";
                }
                else
                {
                    ClienteCombobox.SelectedIndex = -1;
                    MessageBox.Show("No puede seleccionar un cliente diferente si registro ventas de otro cliente en este cobro.", "Operación no válida", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                VentaIdCombobox.ItemsSource = new List<dynamic>() { "Seleccione un cliente para seleccionar una venta." };
            }
        }
    }
}
