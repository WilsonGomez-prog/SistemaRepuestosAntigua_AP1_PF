using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for cUsuarios.xaml
    /// </summary>
    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
            DesdeDataPicker.SelectedDate = Convert.ToDateTime("1/01/0001");
            HastaDataPicker.SelectedDate = DateTime.Now;
        }

        private List<dynamic> GetDisplay(List<Usuarios> lista)
        {
            var listado = new List<dynamic>();

            foreach (var usuario in lista)
            {
                var user = new
                {
                    usuario.UsuarioId,
                    usuario.Nombres,
                    usuario.Apellidos,
                    usuario.Fecha,
                    usuario.NombreUsuario,
                    Permisos = usuario.EsAdmin == 1 ? "Administrador" : "Empleado",
                    UsuarioModificador = usuario.UsuarioModificador != 0 ? UsuariosBLL.Buscar(usuario.UsuarioModificador).NombreUsuario : "Default"
                };

                listado.Add(user);
            }

            return listado;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuarios>();
            List<dynamic> list = new List<dynamic>();
            if (string.IsNullOrWhiteSpace(CriterioTextBox.Text))
            {
                listado = UsuariosBLL.GetList(e => true);
                list = GetDisplay(listado);
            }
            else
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = UsuariosBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate && c.Fecha.Date <= HastaDataPicker.SelectedDate).FindAll(e => e.UsuarioId == Convert.ToInt32(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                    case 1:
                        listado = UsuariosBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate && c.Fecha.Date <= HastaDataPicker.SelectedDate).FindAll(e => e.NombreUsuario.Contains(CriterioTextBox.Text));
                        list = GetDisplay(listado);
                        break;
                }
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = list;
        }
    }
}
