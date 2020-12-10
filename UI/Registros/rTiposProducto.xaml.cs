using BLL;
using System;
using Entidades;
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
    public partial class rTiposProducto : Window
    {
        TiposProducto tiposProducto;
        Usuarios Modificador;
        public rTiposProducto(Usuarios usuario)
        {
            InitializeComponent();
            tiposProducto = new TiposProducto();
            this.DataContext = tiposProducto;
            Modificador = usuario;
        }

        private void Limpiar()
        {
            tiposProducto = new TiposProducto();
            this.DataContext = tiposProducto;
        }

        private bool Validar()
        {
            bool valido = true;

            if (!Utilidades.Utilidades.ValidarCasillaTexto(DescripcionTextBox.Text) || string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
            {
                valido = false;
                MessageBox.Show("La descripcion no puede contener numeros o caracteres especiales o estar vacío.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                DescripcionTextBox.Focus();
            }

            return valido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TipoProductoIdTextBox.Text) || !Char.IsDigit(TipoProductoIdTextBox.Text[0]))
            {
                var tiposProducto = TiposProductoBLL.Buscar(Convert.ToInt32(TipoProductoIdTextBox.Text));

                if (tiposProducto != null)
                {
                    this.tiposProducto = tiposProducto;
                }
                else
                {
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("El ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                TipoProductoIdTextBox.Focus();
            }

            this.DataContext = tiposProducto;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un tipo de producto nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el tipo de producto?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    tiposProducto.UsuarioModificador = Modificador.UsuarioId;
                    bool guardo = TiposProductoBLL.Guardar(tiposProducto);

                    if (guardo)
                    {
                        Limpiar();
                        MessageBox.Show("El tipo de producto ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El tipo de producto no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el tipo de producto?", "Confirmacion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(TipoProductoIdTextBox.Text) || !Char.IsDigit(TipoProductoIdTextBox.Text[0]) || Convert.ToInt32(TipoProductoIdTextBox.Text) == 0)
                {
                    if (TiposProductoBLL.Eliminar(Convert.ToInt32(TipoProductoIdTextBox.Text)))
                    {
                        MessageBox.Show("El tipo de producto ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El tipo de producto no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El ID ingresado no es valido porque contiene letras, caracteres especiales o esta vacio.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    TipoProductoIdTextBox.Focus();
                }
            }
        }
    }
}
