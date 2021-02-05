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

namespace ControlAnimales
{
    /// <summary>
    /// Lógica de interacción para Interface.xaml
    /// </summary>
    public partial class Interface : Window
    {
        public Interface()
        {
            InitializeComponent();

        }

        private void añadir_mascota_Click(object sender, RoutedEventArgs e)
        {

            img_principal.Visibility = Visibility.Hidden;
            agregarMasc.Visibility = Visibility.Visible;
        }

        private void diario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void galeria_Click(object sender, RoutedEventArgs e)
        {

        }



        private void foco(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (txt_nombre.Focus()) {
                txt_nombre.Text = "";
            } else{
                txt_fecha = txt_fecha.Text();
            {
                txt_fecha.Text = "";
            } else if (txt_edad.Focus())
            {
                txt_edad.Text = "";
            }else if (txt_color.Focus()){
                txt_color.Text = "";
            }else if (txt_cartilla.Focus()){
                txt_cartilla.Text = "";
            }else if (txt_chip.Focus()){
                txt_chip.Text = "";
            } else if (txt_fecha_adopcion.Focus()){
                txt_fecha_adopcion.Text = "";
            } else if ( txt_lugar_adopcion.Focus())
            {
                txt_lugar_adopcion.Text = "";
            }
        }

        private void txt_fecha_adopcion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}                         