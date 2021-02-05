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



     /*   private void foco(object sender, DependencyPropertyChangedEventArgs e)
        {
           if (txt_nombre.Focus()) {
                txt_nombre.Text = "";
            } 
            if (txt_fecha.Focus()) {
                txt_fecha.Text = "";
            }  if (txt_edad.Focus())
            {
                txt_edad.Text = "";
            }/*else if (txt_color.Focus()){
                txt_color.Text = "";
            }else if (txt_cartilla.Focus()){
                txt_cartilla.Text = "";
            }
            if (txt_chip.Focus()){
                txt_chip.Text = "";
            }
            if (txt_fecha_adopcion.Focus()){
                txt_fecha_adopcion.Text = "";
            }
            if ( txt_lugar_adopcion.Focus())
            {
                txt_lugar_adopcion.Text = "";
            }
        }*/
    
       

        private void foco(object sender, RoutedEventArgs e)
        {
            TextBox tt = new TextBox();
            tt = (TextBox)sender;
            if (tt.IsFocused) { }
            tt.Text = "";
          /*  MessageBox.Show(tt.Text);
            if (txt_nombre.Focus())
            {
                txt_nombre.Text = "";
            }
            if (txt_fecha.Focus())
            {
                txt_fecha.Text = "";
            }
            if (txt_edad.Focus())
            {
                txt_edad.Text = "";
            }*/
        }
    }
}                         