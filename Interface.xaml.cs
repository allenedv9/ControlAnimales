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

        //Limpia textBox para recojer dato
        private void foco(object sender, RoutedEventArgs e)
        {

            if (txt_nombre.IsFocused)
            {
                txt_nombre.Text = "";
            }
            if (txt_fecha.IsFocused)
            {
                txt_fecha.Text = "";
            }

            if (txt_color.IsFocused)
            {
                txt_color.Text = "";
            }
            if (txt_cartilla.IsFocused)
            {
                txt_cartilla.Text = "";
            }
            if (txt_chip.IsFocused)
            {
                txt_chip.Text = "";
            }
            if (txt_fecha_adopcion.IsFocused)
            {
                txt_fecha_adopcion.Text = "";
            }
            if (txt_lugar_adopcion.IsFocused)
            {
                txt_lugar_adopcion.Text = "";
            }

        }

        //Si el textBox esta vacio vuelve a colocar el nombre del campo a rellenar
        private void perder_foco(object sender, RoutedEventArgs e)
        {
            if (txt_nombre.Text == "")
            {
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
                txt_fecha_adopcion.Text = "Fecha Adopcion";
            }
            if (txt_lugar_adopcion.Text == "")
            {
                txt_lugar_adopcion.Text = "Lugar Adopción";
            }
        }

        //este Metodo calcula la edad de nuestra mascota  meses y años
        private void cargar_fecha_nac(object sender, RoutedEventArgs e)
        {
            //recojo mes y año actual
            String anoActual = DateTime.Today.ToString("yyyy");
            fecha.DisplayDate.Year.ToString();
            fecha.DisplayDate.Month.ToString();
            String mesActual = DateTime.Today.ToString("MM");

            //recojo mes y año nacimiento
            String mesNacimiento = fecha.DisplayDate.Month.ToString();
            String anoNacimiento = fecha.DisplayDate.Year.ToString();
            txt_fecha.Text = anoNacimiento;

       

            //calcula tiempo transcurrido
            int anos = Int32.Parse(anoActual) - Int32.Parse(anoNacimiento);
            int meses = Int32.Parse(mesActual) - Int32.Parse(mesNacimiento);


            //Muestra edad en años y meses y si es menor de un año solo meses
            if (meses < 0)
            {
                anos = anos - 1;
                meses = 12 + meses;

                if (anos == 0)
                {
                    txt_edad.Text = meses.ToString() + " meses";
                }
                else if (anos >= 2)
                {
                    txt_edad.Text = anos.ToString() + " años y " + meses.ToString() + " meses.";
                }
                else
                    txt_edad.Text = anos.ToString() + " año y " + meses.ToString() + " meses.";
            }
            else
            {
                if (anos >= 2)
                {
                    txt_edad.Text = anos.ToString() + " años y " + meses.ToString() + "meses";
                }
                else
                    txt_edad.Text = anos.ToString() + " año y " + meses.ToString() + "meses";
            }

        }

        //Elige la opcion de macho
        private void elige_macho(object sender, RoutedEventArgs e)
        {
            if (macho.IsChecked == true)
            {
                txt_sexo.Text = "macho";
            }
        }

        //Elige la opcion de hembra
        private void elige_hembra(object sender, RoutedEventArgs e)
        {
            if (hembra.IsChecked == true)
            {
                if (hembra.IsChecked == true)
                {
                    txt_sexo.Text = "hembra";
                }
            }
        }

        //Muestra los campos (ocultos)  si es adoptado
        private void ver_txt_adopcion(object sender, RoutedEventArgs e)
        {
            if (adoptado.IsChecked==true) {
                txt_lugar_adopcion.Visibility = Visibility.Visible;
                txt_fecha_adopcion.Visibility = Visibility.Visible;
                dp_fecha_adopcion.Visibility = Visibility.Visible;
            }
            else{
                txt_lugar_adopcion.Visibility = Visibility.Hidden;
                txt_fecha_adopcion.Visibility = Visibility.Hidden;
                dp_fecha_adopcion.Visibility = Visibility.Hidden;
            }
        }

        private void fecha_adopcion(object sender, RoutedEventArgs e)
        {
            String fecha_adopcion = dp_fecha_adopcion.DisplayDate.ToString();
              
            //falta dar formato para que recoja bien dia mes y año
            //solo recoje mes y año
            txt_fecha_adopcion.Text=fecha_adopcion;
           


        }
    }
}
                        