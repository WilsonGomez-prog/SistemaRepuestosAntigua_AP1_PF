using SistemaRepuestosAntigua_AP1_PF.UI;
using SistemaRepuestosAntigua_AP1_PF.UI.Consultas;
using SistemaRepuestosAntigua_AP1_PF.UI.Registros;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaRepuestosAntigua_AP1_PF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rUsuariosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new rUsuarios().Show();
        }

        private void cUsuariosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new cUsuarios().Show();
        }

        private void rClientesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new rClientes().Show();
        }

        private void cClientesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new cClientes().Show();
        }

        private void SalirMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }
}
