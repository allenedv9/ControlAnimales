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


            if (txt_nombre.Focus() || txt_fecha.Focus() || txt_edad.Focus() || txt_color.Focus() ||
                txt_cartilla.Focus() || txt_chip.Focus() || txt_fecha_adopcion.Focus() || txt_lugar_adopcion.Focus())
            {

                //comentario
                txt_nombre.Text = "";
                txt_fecha.Text = "";
                txt_edad.Text = "";
                txt_color.Text = "";
                txt_cartilla.Text = "";
                txt_chip.Text = "";
                txt_fecha_adopcion.Text = "";
                txt_lugar_adopcion.Text = "";

                /////FUNCIONA!!!!
            }
        }

    }
}                         