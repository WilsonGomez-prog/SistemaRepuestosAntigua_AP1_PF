using BLL;
using DAL;
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

namespace SistemaRepuestosAntigua_AP1_PF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rCreditos.xaml
    /// </summary>
    public partial class rCreditos : Window
    {
        Creditos Credito;
        Usuarios Modificador;
        public rCreditos(Usuarios usuario)
        {
            InitializeComponent();

            Credito = new Creditos();
            this.DataContext = Credito;

            Contexto context = new Contexto();
            Modificador = usuario;
            var clientes = (from cli in context.Clientes
                            select new
                            {
                                cli.ClienteId,
                                NombreCompleto = cli.Nombres + " " + cli.Apellidos
                            }).ToList();

            ClienteIdCombobox.ItemsSource = clientes;
            ClienteIdCombobox.SelectedValuePath = "ClienteId";
            ClienteIdCombobox.DisplayMemberPath = "NombreCompleto";
        }

        private void Limpiar()
        {
            Credito = new Creditos();
            this.DataContext = Credito;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(CreditoIdTextbox.Text) || !Char.IsDigit((char)CreditoIdTextbox.Text[0]))
            {
                var credito = CreditosBLL.Buscar(Convert.ToInt32(CreditoIdTextbox.Text));

                if (credito != null)
                {
                    this.Credito = credito;
                    this.DataContext = Credito;
                    ClienteIdCombobox.SelectedIndex = Credito.ClienteId - 1;
                    MessageBox.Show("El crédito que ha buscado ha sido encontrado!!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("El crédito que ha buscado no ha sido encontrado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CreditoIdTextbox.Focus();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea limpiar el formulario para ingresar un producto nuevo? Perderá todos los datos no guardados.", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea guardar el crédito?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (Validar())
                {
                    Credito.ClienteId = Convert.ToInt32(ClienteIdCombobox.SelectedValue);
                    Credito.Balance = Convert.ToSingle(MontoTextbox.Text);
                    Credito.UsuarioModificador = Modificador.UsuarioId;
                    bool guardo = CreditosBLL.Guardar(Credito);

                    if (guardo)
                    {
                        Limpiar();
                        MessageBox.Show("El crédito ha sido guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("El crédito no ha podido ser guardado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿De verdad desea eliminar el el crédito?", "Confirmacion", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                if (!Char.IsDigit((char)CreditoIdTextbox.Text[0]) || !string.IsNullOrWhiteSpace(CreditoIdTextbox.Text))
                {
                    if (CreditosBLL.Eliminar(Convert.ToInt32(CreditoIdTextbox.Text)))
                    {
                        MessageBox.Show("El crédito ha sido eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El crédito no pudo ser eliminado.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    CreditoIdTextbox.Focus();
                }
            }

            
        }

        private bool Validar()
        {
            bool valido = true;


            if (!Char.IsDigit((char)CreditoIdTextbox.Text[0]))
            {
                MessageBox.Show("El ID que ha ingresado no es válido, no puede contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                CreditoIdTextbox.Focus();
            }
            else if (ClienteIdCombobox.SelectedItem == null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un cliente para asignar el crédito.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClienteIdCombobox.Focus();
            }
            else if (!Utilidades.Utilidades.ValidarCasillaNumerica(MontoTextbox.Text))
            {
                valido = false;
                MessageBox.Show("El monto no debe de contener letras o caracteres especiales.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                MontoTextbox.Focus();
            }
            else if (CreditosBLL.GetList(c => c.ClienteId == Convert.ToInt32(ClienteIdCombobox.SelectedValue)).FirstOrDefault() != null)
            {
                valido = false;
                MessageBox.Show("Debe de seleccionar un cliente que no tenga un crédito registrado \npara poder asignar el crédito o busque al credito \nperteneciente al cliente para modificarle.", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                ClienteIdCombobox.Focus();
            }

            return valido;
        }
    }
}
