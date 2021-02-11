using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection con;
        public Interface()
        {
            InitializeComponent();

            //conexion AIDA
             //string conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aida\\Desktop\\PERRUNO\\ControlAnimales\\Mascotas.mdf;Integrated Security=True;Connect Timeout=30";
           
            //conexion ALLENDE
            string conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Allende\\source\\repos\\ControlAnimales\\Mascotas.mdf;Integrated Security=True;Connect Timeout=30";
           con = new SqlConnection(conexion);
           cargarEspecies();
        }

        private void añadir_mascota_Click(object sender, RoutedEventArgs e){
                 img_principal.Visibility = Visibility.Hidden;
                agregarMasc.Visibility = Visibility.Visible;
            

            if(veterinaria.Visibility == Visibility.Visible)
            {
                veterinaria.Visibility = Visibility.Hidden;
            }
        }

        private void abrir_veterinario(object sender, RoutedEventArgs e)  {
            agregarMasc.Visibility = Visibility.Hidden;
            veterinaria.Visibility = Visibility.Visible;
        }
        private void diario_Click(object sender, RoutedEventArgs e){

        }

        private void galeria_Click(object sender, RoutedEventArgs e){

        }

        //Limpia textBox para recojer dato
        private void foco(object sender, RoutedEventArgs e){

            if (txt_nombre.IsFocused){

                txt_nombre.Text = "";
            }

            if (txt_fecha.IsFocused){

                txt_fecha.Text = "";
            }

            if (txt_color.IsFocused){

                txt_color.Text = "";
            }

            if (txt_cartilla.IsFocused){

                txt_cartilla.Text = "";
            }

            if (txt_chip.IsFocused){

                txt_chip.Text = "";
            }

            if (txt_fecha_adopcion.IsFocused){

                txt_fecha_adopcion.Text = "";
            }

            if (txt_lugar_adopcion.IsFocused){

                txt_lugar_adopcion.Text = "";
            }
           if (txt_nombre_veterinaria.IsFocused) {
                txt_nombre_veterinaria.Text = "";
            }
            if (txt_clinica.IsFocused)
            {
                txt_clinica.Text = "";
            }
            if (txt_calle_veterinaria.IsFocused)
            {
                txt_calle_veterinaria.Text = "";
            }
            if (txt_telefono_clinica.IsFocused)
            {
                txt_telefono_clinica.Text = "";
            }
            if (txt_telefono_urgencias.IsFocused)
            {
                txt_telefono_urgencias.Text = "";
            }
        }

        //Si el textBox esta vacio vuelve a colocar el nombre del campo a rellenar
        private void perder_foco(object sender, RoutedEventArgs e) {

            if (txt_nombre.Text == ""){

                txt_nombre.Text = "Nombre";
            }

            if (txt_fecha.Text == ""){

                txt_fecha.Text = "Fecha Nacimineto";
            }

            if (txt_edad.Text == ""){

                txt_edad.Text = "Edad";
            }

            if (txt_color.Text == ""){

                txt_color.Text = "Color";
            }

            if (txt_cartilla.Text == ""){

                txt_cartilla.Text = "Cartilla";
            }

            if (txt_chip.Text == ""){

                txt_chip.Text = "Chip";
            }

            if (txt_fecha_adopcion.Text == ""){

                txt_fecha_adopcion.Text = "Fecha Adopcion";
            }

            if (txt_lugar_adopcion.Text == ""){

                txt_lugar_adopcion.Text = "Lugar Adopción";
            }
            if (txt_nombre_veterinaria.Text == "")
            {
                txt_nombre_veterinaria.Text = "Nombre Veterinari@";
            }
            if (txt_clinica.Text == "")
            {
                txt_clinica.Text = "Nombre Clinica";
            }
            if (txt_calle_veterinaria.Text == "")
            {
                txt_calle_veterinaria.Text = "Calle";
            }
            if (txt_telefono_clinica.Text == "")
            {
                txt_telefono_clinica.Text = "Teléfono Clínica";
            }
            if (txt_telefono_urgencias.Text == "")
            {
                txt_telefono_urgencias.Text = "Teléfono Urgencias";
            }
        }

        //este Metodo calcula la edad de nuestra mascota  meses y años
        private void cargar_fecha_nac(object sender, RoutedEventArgs e){

            //recojo mes y año actual
            String anoActual = DateTime.Today.ToString("yyyy");
            String mesActual = DateTime.Today.ToString("MM");

            //ESTO NO HACE FALTA, funciona si esto, no lo borro de momento por si acaso
            //  fecha.DisplayDate.Year.ToString();
            // fecha.DisplayDate.Month.ToString();

            //recojo mes y año nacimiento
            String mesNacimiento = fecha.DisplayDate.Month.ToString();
            String anoNacimiento = fecha.DisplayDate.Year.ToString();
            txt_fecha.Text = anoNacimiento;

            //calcula tiempo transcurrido
            int anos = Int32.Parse(anoActual) - Int32.Parse(anoNacimiento);
            int meses = Int32.Parse(mesActual) - Int32.Parse(mesNacimiento);


            //Si solo tiene meses , muestra meses
            if (meses < 0){

                anos = anos - 1;
                meses = 12 + meses;

                if (anos == 0){

                    txt_edad.Text = meses.ToString() + " meses";

                }else if (anos >= 2){

                    txt_edad.Text = anos.ToString() + " años y " + meses.ToString() + " meses.";

                     }else{

                    txt_edad.Text = anos.ToString() + " año y " + meses.ToString() + " meses.";
                     }
                }

            //Si ya tiene un año o mas, muestra años y meses
            else {
                if (anos >= 2){

                    txt_edad.Text = anos.ToString() + " años y " + meses.ToString() + "meses";
                } else {

                    txt_edad.Text = anos.ToString() + " año y " + meses.ToString() + "meses";
                }
            }

        }

        //RadioButton para el sexo
        private void selecciona_sexo(object sender, RoutedEventArgs e)
        {
            if (macho.IsChecked == true)
            {
                txt_sexo.Text = "macho";
            }
            else
            {
                txt_sexo.Text = "hembra";
            }

        }


        //Muestra los campos (ocultos)  si es adoptado
        private void ver_txt_adopcion(object sender, RoutedEventArgs e)
        {
            if (adoptado.IsChecked == true)
            {
                txt_lugar_adopcion.Visibility = Visibility.Visible;
                txt_fecha_adopcion.Visibility = Visibility.Visible;
                dp_fecha_adopcion.Visibility = Visibility.Visible;
            }
            else
            {
                txt_lugar_adopcion.Visibility = Visibility.Hidden;
                txt_fecha_adopcion.Visibility = Visibility.Hidden;
                dp_fecha_adopcion.Visibility = Visibility.Hidden;
            }
        }

        //Recoje fecha de adopción
        private void fecha_adopcion(object sender, RoutedEventArgs e)
        {
            String fecha_adopcion = dp_fecha_adopcion.SelectedDate.ToString();
            txt_fecha_adopcion.Text = fecha_adopcion;

        }

        //abre ventana de dialogo para seleccionar imagen si lo desea
        private void abre_seleccion_imagen(object sender, RoutedEventArgs e)
        {
            if (check_imagen.IsChecked == true)
            {
                //Cuadro de dialogo para imcluir imagen mascota
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog {
                    // Set filter for file extension and default file extension 
                    DefaultExt = ".jpg",
                    Filter = "Todos (*.*)|*.*|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg"
                };

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true) {
                    string imagenFichero = dlg.FileName;

                    ruta_imagen.Text = imagenFichero;
                    BitmapImage bitmap;
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagenFichero);
                    bitmap.EndInit();
                    imagenMascota.Source = bitmap;
                }
            }
        }
    
        //rellena el combo box de especies con los datos de bd
        private void cargarEspecies() {

            try{
                //abrimos la conexion
                con.Open();
            }catch (Exception ee){
                MessageBox.Show(ee.Message);
            }

            string query = "SELECT * FROM especie";
            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable especies = new DataTable();
                miAdaptadorSql.Fill(especies);
                especie.DisplayMemberPath = "especie";
                especie.SelectedValuePath = "id_especie";
                especie.ItemsSource = especies.DefaultView;
            }

           
               con.Close();
        }

      

        private void txt_calle_veterinaria_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cargaRazas(object sender, SelectionChangedEventArgs e)
        {

            ComboBox cb = (ComboBox)sender;

            DataRowView drv = (DataRowView)cb.SelectedItem;
            string cadena = drv.Row[0].ToString();
            cadena.ToLower();
            MessageBox.Show(cadena);

            razas.Visibility = Visibility.Visible;

            try
            {
                //abrimos la conexion
                con.Open();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            if(cadena == "perro"){

            }
            string query = "SELECT * FROM " + cadena;
            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable especies = new DataTable();
                miAdaptadorSql.Fill(especies);
                especie.DisplayMemberPath = "raza";
                especie.SelectedValuePath = "id_especie";
                especie.ItemsSource = especies.DefaultView;
            }

            
            con.Close();*/
        }
    }
}             

