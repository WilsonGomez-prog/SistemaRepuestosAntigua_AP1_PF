﻿using BLL;
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

namespace SistemaRepuestosAntigua_AP1_PF.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            NombreUsuarioTextBox.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void IngresarButton_Click(object sender, RoutedEventArgs e)
        {
            bool valido = UsuariosBLL.Validar(NombreUsuarioTextBox.Text, ClavePasswordBox.Password);

            if(valido)
            {
                new MainWindow(UsuariosBLL.Buscar(NombreUsuarioTextBox.Text, ClavePasswordBox.Password)).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario o la contraseña ingresada son erroneos, intente denuevo!", "Error!");
                NombreUsuarioTextBox.Focus();
                ClavePasswordBox.Clear();
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ClaveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClavePasswordBox.Password = ClaveTextBox.Text;
        }

        private void VisualizarClaveButton_Click(object sender, RoutedEventArgs e)
        {
            Utilidades.Utilidades.VisualizarClave(ref VisualizarClaveButton, ref OcultarClaveButton, ref ClavePasswordBox, ref ClaveTextBox);
        }

        private void OcultarClaveButton_Click(object sender, RoutedEventArgs e)
        {
            Utilidades.Utilidades.OcultarClave(ref VisualizarClaveButton, ref OcultarClaveButton, ref ClavePasswordBox, ref ClaveTextBox);
        }
    }
}
