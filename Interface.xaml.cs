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
        DatePicker datPicker=new DatePicker();
        public Interface()
        {
            InitializeComponent();

          //  datPicker =fecha.SelectedDate.Value.ToString();

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



        private void foco(object sender, RoutedEventArgs e)
        {
       
            if (txt_nombre.IsFocused) {
                txt_nombre.Text = "";
            }
            if (txt_fecha.IsFocused){
                txt_fecha.Text = "";
            }
            if (txt_edad.IsFocused) {
                txt_edad.Text = "";
            }
            if (txt_edad.IsFocused){
                txt_edad.Text = "";
            }
            if (txt_color.IsFocused) {
                txt_color.Text = "";
            }
            if (txt_cartilla.IsFocused){
                txt_cartilla.Text = "";
            }
            if (txt_chip.IsFocused) {
                txt_chip.Text = "";
            }
            if (txt_fecha_adopcion.IsFocused){
                txt_fecha_adopcion.Text = "";
            }
            if (txt_lugar_adopcion.IsFocused){
                txt_lugar_adopcion.Text = "";
            }
           
        }

     

        private void cargar_fecha_nac(object sender, RoutedEventArgs e)
        {
           // DateTime fecha_actual = DateTime.Parse(txt_fecha.Text);
           // MessageBox.Show(fecha_actual.ToString());
             txt_fecha.Text = fecha.DisplayDate.ToString();
             txt_edad.Text = fecha.DisplayDate.Year.ToString();
    
        }

        private void perder_foco(object sender, RoutedEventArgs e)
        {
            if (txt_nombre.Text==""){
                txt_nombre.Text = "Nombre";
            }
            if (txt_fecha.Text == "")
            {
                txt_fecha.Text = "Fecha Nacimineto";
            }
            if (txt_edad.Text == "")
            {
                txt_edad.Text = "Edad";
            }
            if (txt_color.Text == "")
            {
                txt_color.Text = "Color";
            }
            if (txt_cartilla.Text == "")
            {
                txt_cartilla.Text = "Cartilla";
            }
            if (txt_chip.Text == "")
            {
                txt_chip.Text = "Chip";
            }
            if (txt_fecha_adopcion.Text == "")
            {
                txt_fecha_adopcion.Text = "";
            }
            if (txt_lugar_adopcion.Text == "")
            {
                txt_lugar_adopcion.Text = "Lugar Adopción";
            }
        }

        
    }
}                         