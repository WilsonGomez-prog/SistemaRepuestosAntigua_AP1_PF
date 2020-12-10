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

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rProductos.xaml
    /// </summary>
    public partial class rProductos : Window
    {
        Productos Producto; 
        Usuarios Modificador;
        public rProductos(Usuarios usuario)
        {
            InitializeComponent();
            Producto = new Productos();
            this.DataContext = Producto;
            Modificador = usuario;

            TipoProductoIdCombobox.ItemsSource = TiposProductoBLL.GetList(tp => true);
            TipoProductoIdCombobox.SelectedValuePath = "TipoProductoId";
            TipoProductoIdCombobox.DisplayMemberPath = "Descripcion";

            var us = new { EstadoId = 1, Descripcion = "Usado" };
            var nu = new { EstadoId = 0, Descripcion = "Nuevo" };

            EstadoProductoCombobox.ItemsSource = new List<dynamic>() { nu, us };
            EstadoProductoCombobox.SelectedValuePath = "EstadoId";
            EstadoProductoCombobox.DisplayMemberPath = "Descripcion";

            var ex = new { ImpId = 0, Descripcion = "Execnto(0%)" };
            var imp1 = new { ImpId = 12, Descripcion = "12%" };
            var imp2 = new { ImpId = 18, Descripcion = "18%" };

            ImpuestoComboBox.ItemsSource = new List<dynamic>() { ex, imp1, imp2 };
            ImpuestoComboBox.SelectedValuePath = "ImpId";
            ImpuestoComboBox.DisplayMemberPath = "Descripcion";
        }

        private void Limpiar()
        {
            Producto = new Productos();
            this.DataContext = Producto;
        }

        public bool Validar()
        {
            bool valido = true;

            if (!Utilidades.Utilidades.ValidarCasillaAlfaNumerica(DescripcionTextBox.Text) || string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla descripción no puede tener caracteres especiales o estar vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DescripcionTextBox.Focus();
            }
            else if (TipoProductoIdCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un tipo de producto.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                TipoProductoIdCombobox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(PrecioUnitTextBox.Text) || string.IsNullOrWhiteSpace(PrecioUnitTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El precio unitario no puede llevar letras o caracteres especiales o estar vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                PrecioUnitTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaAlfaNumerica(CodigoTextBox.Text) || string.IsNullOrWhiteSpace(CodigoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La casilla codigo no puede tener caracteres especiales o estar vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CodigoTextBox.Focus();
            }
            else if (ProductosBLL.Existe(Convert.ToInt32(ProductoIdTextBox.Text), CodigoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El producto que esta intentando registrar ya esta registrado con otro producto.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DescripcionTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(DescuentoTextBox.Text) || string.IsNullOrWhiteSpace(DescuentoTextBox.Text))
            {
                valido = false;
                MessageBox.Show("El % de descuento no debe de contener letras o caracteres especiales o estar vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DescuentoTextBox.Focus();
            }
            else if (Convert.ToSingle(DescuentoTextBox.Text) > 60)
            {
                valido = false;
                MessageBox.Show("El % de descuento introducido no debe ser mayor del 60%.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DescuentoTextBox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(ExistenciaTextBox.Text) || string.IsNullOrWhiteSpace(ExistenciaTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La cantidad de existencias no debe de contener letras o caracteres especiales o estar vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ExistenciaTextBox.Focus();
            }
            else if (EstadoProductoCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de indicar el estado del producto.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                EstadoProductoCombobox.Focus();
            }
            else if (ImpuestoComboBox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar el porcentaje de impuesto que se le aplica al producto.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ImpuestoComboBox.Focus();
            }

            return valido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProductoIdTextBox.Text) || !Char.IsDigit((char)ProductoIdTextBox.Text[0]))
            {
                var producto = ProductosBLL.Buscar(Convert.ToInt32(ProductoIdTextBox.Text));

                if (producto != null)
                {
                    Producto = producto;
                }
                else
                {
                    Limpiar();
                }

                this.DataContext = Producto;
                TipoProductoIdCombobox.SelectedIndex = Producto.TipoProductoId;
                EstadoProductoCombobox.SelectedIndex = Producto.EstadoProducto;
                ImpuestoComboBox.SelectedIndex = (int)Producto.Descuento;
               
            }
            else
            {
                MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ProductoIdTextBox.Focus();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un producto nuevo? Perderá todos los datos no guardados."  , "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el producto?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    Producto.UsuarioModificador = Modificador.UsuarioId;
                    Producto.TipoProductoId = Convert.ToInt32(TipoProductoIdCombobox.SelectedValue);
                    Producto.Impuesto = Convert.ToSingle(ImpuestoComboBox.SelectedValue);
                    bool guardo = ProductosBLL.Guardar(Producto);

                    if (guardo)
                    {
                        Limpiar();
                        MessageBox.Show("El producto ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El producto no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el producto?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(ProductoIdTextBox.Text) || !Char.IsDigit((char)ProductoIdTextBox.Text[0]) || Convert.ToInt32(ProductoIdTextBox.Text) == 0)
                {
                    if (ProductosBLL.Eliminar(Convert.ToInt32(ProductoIdTextBox.Text)))
                    {
                        MessageBox.Show("El producto ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El producto no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    ProductoIdTextBox.Focus();
                }
            }
        }
    }
}
