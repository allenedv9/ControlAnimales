using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        String conexion, cadena2, cadena3, cadena5, nombre, fechaNac, especieAni, edad, raza,
               sexo, color, cartilla, numChip, adoptadoCheck, lugarAdop, fechaAdop, ruta, id;
        String nombreVeterinaria, clinica, calle, localidad, telefono, telefonoUrgencias, cad, idMAsc, cadNombre;
        String visFecha, visDescripcion, visPrecio, visClinica, visIdMascota, visIdVeterinario, visTratamiento, visDuracion, visDescripcionTratamiento, tratamientoChek;



        private DataRow[] filas;
        private Veterinaria registro;

        private Mascota registroM;

        public Interface()
        {
            InitializeComponent();

            //  conexion AIDA 
            conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aida\\Desktop\\PERRUNO\\ControlAnimales\\Mascotas.mdf;Integrated Security=True;Connect Timeout=30";
            //  conexion ALLENDE
            //    conexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Allende\\source\\repos\\ControlAnimales\\Mascotas.mdf;Integrated Security=True;Connect Timeout=30";

            con = new SqlConnection(conexion);
            cargarEspecies();
            cargaMascotas();
            cargaMascotasVet();
            cargarVeterinario();
            cargarMascotasDataGrid();
            cargarVeterinarioGrid();

        }

        private void eliminar_mascotas(object sender, RoutedEventArgs e)
        {
            nombre = txt_nombre.Text;
            con = new SqlConnection(conexion);
            con.Open();
            String query = "DELETE from mascota where nombre = '" + nombre + "'";
            SqlCommand comando = new SqlCommand(query, con);
            comando.ExecuteNonQuery();
            MessageBox.Show("Datos Borrados");
            con.Close();
            LimpiarControles();


        }

        /*****************************************************************************************************************************/
        //INSERT UPDATE DELETE SELECT DE LA PANTALLA VISITAS VETERINARIO
        /*****************************************************************************************************************************/
        private void guardar_visita_vet(object sender, RoutedEventArgs e)
        {

            visFecha = txt_fecha_visita.Text;
            visDescripcion = txt_descripcion_visita.Text;
            visPrecio = txt_precio_visita.Text;
            visClinica = txt_clinica.Text;
            visIdMascota = txt_mascota_visita.Text;
            visIdVeterinario = txt_clinica_vet.Text;
            visTratamiento = txt_tratamiento.Text;
            visDuracion = txt_duracion.Text;
            visDescripcionTratamiento = txt_descripcion_visita.Text;
            visFecha = visFecha.Substring(0, 10);


            con = new SqlConnection(conexion);
            String query = "Insert into visitas_veterinario values( '" + visFecha + "','" + visDescripcion + "', '" + visPrecio + "', '" + visIdMascota + "', '" + visIdVeterinario + "', '" + tratamientoChek + "', '" + visDuracion + "', '" + visDescripcionTratamiento + "')";
            SqlCommand comando = new SqlCommand(query, con);
            con.Open();
            comando.ExecuteNonQuery();
            MessageBox.Show("Datos insertados");
            con.Close();

        }

        //recojo dia mes y año visita veterinario
        private void cargar_fecha_vis(object sender, RoutedEventArgs e)
        {
            String fecha_visita = fecha_vis.SelectedDate.ToString();
            txt_fecha_visita.Text = fecha_visita;
        }

        private void txt_clinica_vet_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        /*****************************************************************************************************************************/
        //INSERT UPDATE DELETE SELECT DE LA PANTALLA MASCOTAS
        /*****************************************************************************************************************************/

        private void Modificar_mascotas(object sender, RoutedEventArgs e)
        {
            nombre = txt_nombre.Text;
            fechaNac = txt_fecha.Text;
            edad = txt_edad.Text;
            raza = txt_raza.Text;
            sexo = txt_sexo.Text;
            especieAni = txt_especie.Text;
            color = txt_color.Text;
            cartilla = txt_cartilla.Text;
            numChip = txt_chip.Text;
            fechaAdop = txt_fecha_adopcion.Text;
            lugarAdop = txt_lugar_adopcion.Text;
            ruta = ruta_imagen.Text;
            idMAsc = txt_id.Text;


            con = new SqlConnection(conexion);
            con.Open();
            try
            {

                String query = "UPDATE mascota set nombre = '" + nombre + "', fecha_nacimiento = '" + fechaNac + "', edad = '" + edad + "', especie = '" + especieAni + "', raza = '" + raza + "', sexo = '" + sexo + "', color = '" + color + "', num_cartilla_sanitaria = '" + cartilla + "', num_chip = '" + numChip + "', adoptado = '" + adoptadoCheck + "', fecha_adopcion = '" + fechaAdop + "', lugar_adopcion = '" + lugarAdop + "', imagen = '" + ruta + "' WHERE id_mascota = '" + idMAsc + "'";
               // MessageBox.Show(query);
                SqlCommand comando = new SqlCommand(query, con);
                comando.ExecuteNonQuery();
                MessageBox.Show("Datos Actualizados");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido realizar la actualización de los datos");
            }


            con.Close();
            LimpiarControles();
        }

        private void Buscar_mascota(object sender, RoutedEventArgs e)
        {

            txt_especie.Visibility = Visibility.Visible;
            txt_fecha.Visibility = Visibility.Visible;
            txt_raza.Visibility = Visibility.Visible;
            txt_sexo.Visibility = Visibility.Visible;

            con.Open();

            if (txt_fecha.Text != "")
            {
                txt_fecha.Text = txt_fecha.Text.Substring(0, 10).ToString();
            }

            if (txt_fecha_adopcion.Text != "")
            {
                txt_fecha_adopcion.Text = txt_fecha_adopcion.Text.Substring(0, 10).ToString();
            }
            SqlCommand comando = new SqlCommand("SELECT * FROM mascota where nombre = @mascota", con);
            comando.Parameters.AddWithValue("@mascota", txt_nombre.Text);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                txt_nombre.Text = registro["nombre"].ToString();
                txt_fecha.Text = registro["fecha_nacimiento"].ToString();
                txt_edad.Text = registro["edad"].ToString();
                txt_raza.Text = registro["raza"].ToString();
                txt_sexo.Text = registro["sexo"].ToString();
                txt_especie.Text = registro["especie"].ToString();
                txt_color.Text = registro["color"].ToString();
                txt_cartilla.Text = registro["num_cartilla_sanitaria"].ToString();
                txt_chip.Text = registro["num_chip"].ToString();
                adoptadoCheck = registro["adoptado"].ToString();
                txt_id.Text = registro["id_mascota"].ToString();
                ruta_imagen.Text = registro["imagen"].ToString();

                if (adoptadoCheck == "si")
                {

                    txt_nombre.Text = registro["nombre"].ToString();
                    txt_fecha.Text = registro["fecha_nacimiento"].ToString();
                    txt_edad.Text = registro["edad"].ToString();
                    txt_raza.Text = registro["raza"].ToString();
                    txt_sexo.Text = registro["sexo"].ToString();
                    txt_especie.Text = registro["especie"].ToString();
                    txt_color.Text = registro["color"].ToString();
                    txt_cartilla.Text = registro["num_cartilla_sanitaria"].ToString();
                    txt_chip.Text = registro["num_chip"].ToString();
                    adoptadoCheck = registro["adoptado"].ToString();
                    txt_id.Text = registro["id_mascota"].ToString();
                    if (adoptadoCheck == "si")
                    {
                        txt_fecha_adopcion.Visibility = Visibility.Visible;
                        label_fecha.Visibility = Visibility.Visible;
                        label_lugar.Visibility = Visibility.Visible;
                        txt_lugar_adopcion.Visibility = Visibility.Visible;

                        txt_fecha_adopcion.Text = registro["fecha_adopcion"].ToString();
                        txt_lugar_adopcion.Text = registro["lugar_adopcion"].ToString();

                    }
                    else
                    {
                        txt_fecha_adopcion.Visibility = Visibility.Hidden;
                        label_fecha.Visibility = Visibility.Hidden;
                        label_lugar.Visibility = Visibility.Hidden;
                        txt_lugar_adopcion.Visibility = Visibility.Hidden;
                    }
                    // txt_lugar_adopcion.Text = registro["lugar_adopcion"].ToString();
                    txt_fecha_adopcion.Text = registro["fecha_adopcion"].ToString();
                    ruta_imagen.Text = registro["imagen"].ToString();
                    check_imagen.Visibility = Visibility.Hidden;
                    selec_img.Visibility = Visibility.Hidden;
                }

                con.Close();

            
        }

           
        }
        private void guardar_Click(object sender, RoutedEventArgs e)
        {

            nombre = txt_nombre.Text;
            fechaNac = txt_fecha.Text;
            edad = txt_edad.Text;
            especieAni = txt_especie.Text;
            raza = txt_raza.Text;
            sexo = txt_sexo.Text;
            color = txt_color.Text;
            cartilla = txt_cartilla.Text;
            numChip = txt_chip.Text;
            lugarAdop = txt_lugar_adopcion.Text;
            fechaAdop = txt_fecha_adopcion.Text;
            ruta = ruta_imagen.Text;
            id = txt_id.Text;
            if (fechaNac != "")
            {
                fechaNac = fechaNac.Substring(0, 10).ToString();
            }

            if (fechaAdop != "")
            {
                fechaAdop = fechaAdop.Substring(0, 10).ToString();
            }

            try
            {
                con = new SqlConnection(conexion);
                con.Open();
                String query = "Insert into mascota values( '" + nombre + "','" + fechaNac + "', '" + edad + "','" + especieAni + "', '" + raza + "','" + sexo + "', '" + color + "','" + cartilla + "','" + numChip + "', '" + adoptadoCheck + "', '" + fechaAdop + "','" + lugarAdop + "', '" + ruta + "')";
                SqlCommand comando = new SqlCommand(query, con);
                comando.ExecuteNonQuery();
                MessageBox.Show("Datos insertados");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            con.Close();
            LimpiarControles();
            cargarMascotasDataGrid();
        }



        private void recogerDato(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            DataRowView drv = (DataRowView)cb.SelectedItem;
            string cadena3 = drv.Row[1].ToString();
            txt_raza.Text = cadena3;
        }



        /*****************************************************************************************************************************/
        //MODIFICAR LOS DATOS DE LA PANTALLA VETERINARIO//
        /*****************************************************************************************************************************/

        //carga el dataGrid con datos de ta tabla veterinario
        private void cargarVeterinarioGrid()
        {
            con.Open();
            String query = "Select * from veterinario";

            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable dataTablaVeterinario = new DataTable();
                miAdaptadorSql.Fill(dataTablaVeterinario);

                dg_lista_veterinario.ItemsSource = dataTablaVeterinario.DefaultView;
            }
            con.Close();
        }

        //carga el dataGrid con datos de ta tabla mascotas
        public void cargarMascotasDataGrid()
        {
            con.Open();
            String query = "Select * from mascota";

            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable dataTablaMascota = new DataTable();
                miAdaptadorSql.Fill(dataTablaMascota);

                dg_lista_mascotas.ItemsSource = dataTablaMascota.DefaultView;
            }
            con.Close();
        }

        //carga combo de mascotas en vista veterinario
        private void cargaMascotasVet()
        {
            try
            {
                //abrimos la conexion
                con.Open();
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
            string query = "SELECT * FROM mascota";
            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable mascota = new DataTable();
                miAdaptadorSql.Fill(mascota);
                nombreMascotasVet.DisplayMemberPath = "nombre";
                nombreMascotasVet.SelectedValuePath = "id_mascota";
                nombreMascotasVet.ItemsSource = mascota.DefaultView;
            }
            con.Close();
        }

        //carga combo de veterinario en vista veterinario
        private void cargarVeterinario()
        {
            try
            {
                //abrimos la conexion
                con.Open();
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
            string query = "SELECT * FROM veterinario";
            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable veterinario = new DataTable();
                miAdaptadorSql.Fill(veterinario);
                carga_vet.DisplayMemberPath = "clinica";
                carga_vet.SelectedValuePath = "id_veterinario";
                carga_vet.ItemsSource = veterinario.DefaultView;
            }
            con.Close();
        }

        //carga combo
        private void cargar_masc_veterinario(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            DataRowView drv = (DataRowView)cb.SelectedItem;
            cadena5 = drv.Row[0].ToString();
            txt_mascota_visita.Text = cadena5;
        }


        private void Clinica_selescciona(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            DataRowView drv = (DataRowView)cb.SelectedItem;
            string cadena3 = drv.Row[0].ToString();
            txt_clinica_vet.Text = cadena3;
        }
        //INSERT VETERINARIO
        private void Guardar_Veterinaria(object sender, RoutedEventArgs e)
        {
            nombreVeterinaria = txt_nombre_veterinaria.Text;
            clinica = txt_clinica.Text;
            calle = txt_calle_veterinaria.Text;
            localidad = txt_localidad.Text;
            telefono = txt_telefono_clinica.Text;
            telefonoUrgencias = txt_telefono_urgencias.Text;
            string v = txtId.Text;

            con = new SqlConnection(conexion);
            String query = "Insert into veterinario values( '" + clinica + "','" + calle + "', '" + localidad + "', '" + nombreVeterinaria + "', '" + telefono + "', '" + telefonoUrgencias + "', '" + v + "')";
            SqlCommand comando = new SqlCommand(query, con);
            con.Open();
            comando.ExecuteNonQuery();
            MessageBox.Show("Datosa insertados");
            con.Close();


        }
        //Eliminar veterinaria
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            nombreVeterinaria = txt_nombre_veterinaria.Text;
            clinica = txt_clinica.Text;
            calle = txt_calle_veterinaria.Text;
            localidad = txt_localidad.Text;
            telefono = txt_telefono_clinica.Text;
            telefonoUrgencias = txt_telefono_urgencias.Text;

            con = new SqlConnection(conexion);
            con.Open();
            String query = "DELETE from veterinario where clinica = '" + clinica + "'";
            SqlCommand comando = new SqlCommand(query, con);
            comando.ExecuteNonQuery();
            MessageBox.Show("Datos Borrados");
            con.Close();
        }

        //CARGAR MASCOTAS EN EL COMBOBOX
        private void cargaMascotas()
        {

            try
            {
                //abrimos la conexion
                con.Open();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            string query = "SELECT * FROM mascota";
            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(query, con);

            using (miAdaptadorSql)
            {
                DataTable mascota = new DataTable();
                miAdaptadorSql.Fill(mascota);
                nombreMascotas.DisplayMemberPath = "nombre";
                nombreMascotas.SelectedValuePath = "id_mascota";
                nombreMascotas.ItemsSource = mascota.DefaultView;

            }


            con.Close();
        }

        private void mascVet(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            DataRowView drv = (DataRowView)cb.SelectedItem;
            cad = drv.Row[0].ToString();
            cadNombre = drv.Row[1].ToString();
            //MessageBox.Show(cad);
            txtId.Text = cad;
        }



        //Modifica Veterinaria
        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            nombreVeterinaria = txt_nombre_veterinaria.Text;
            clinica = txt_clinica.Text;
            calle = txt_calle_veterinaria.Text;
            localidad = txt_localidad.Text;
            telefono = txt_telefono_clinica.Text;
            telefonoUrgencias = txt_telefono_urgencias.Text;



            con = new SqlConnection(conexion);
            con.Open();
            try
            {

                String query = "UPDATE veterinario set clinica = '" + clinica + "', calle = '" + calle + "', localidad = '" + localidad + "', nombre_veterinario = '" + nombreVeterinaria + "', telefono= '" + telefono + "',telefono_urgencias = '" + telefonoUrgencias + "' WHERE id_mascota =  '" + idMAsc;
                SqlCommand comando = new SqlCommand(query, con);
                comando.ExecuteNonQuery();
                MessageBox.Show("Datos Actualizados");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encuentran datos para su busqueda" + ex);
            }

            con.Close();
        }

        //Busca veterinaria
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand comando = new SqlCommand("SELECT * FROM veterinario where id_mascota = @mascota", con);
            comando.Parameters.AddWithValue("@mascota", txtId.Text);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                txt_calle_veterinaria.Text = registro["calle"].ToString();
                txt_clinica.Text = registro["clinica"].ToString();
                txt_localidad.Text = registro["localidad"].ToString();
                txt_nombre_veterinaria.Text = registro["nombre_veterinario"].ToString();
                txt_telefono_clinica.Text = registro["telefono"].ToString();
                txt_telefono_urgencias.Text = registro["telefono_urgencias"].ToString();
            }
            con.Close();

        }

        //LIMPIAR LOS CONTROLES VETERINARIO/VISITAS_VETERINARIO/MASCOTA
        private void LimpiarControles_Click(object sender, RoutedEventArgs e)
        {
            //Veterinario
            txt_nombre_veterinaria.Text = "";
            txt_clinica.Text = "";
            txt_calle_veterinaria.Text = "";
            txt_localidad.Text = "";
            txt_telefono_clinica.Text = "";
            txt_telefono_urgencias.Text = "";
            txt_clinica_vet.Text = "";
            //Visitas veterinario
            txt_descripcion_visita.Text = "";
            txt_precio_visita.Text = "";
            txt_tratamiento.Text = "";
            txt_duracion.Text = "";
            //Mascota
            txt_nombre.Text = "";
            txt_fecha.Text = "";
            txt_edad.Text = "";
            txt_especie.Text = "";
            txt_raza.Text = "";
            txt_sexo.Text = "";
            txt_color.Text = "";
            txt_cartilla.Text = "";
            txt_chip.Text = "";
            txt_lugar_adopcion.Text = "";
            txt_fecha_adopcion.Text = "";
            ruta_imagen.Text = "";
            txt_id.Text = "";
            imagenMascota = null;

            txt_fecha_adopcion.Visibility = Visibility.Hidden;
            label_fecha.Visibility = Visibility.Hidden;
            label_lugar.Visibility = Visibility.Hidden;
            txt_raza.Visibility = Visibility.Hidden;
            txt_sexo.Visibility = Visibility.Hidden;
            txt_especie.Visibility = Visibility.Hidden;
            txt_fecha.Visibility = Visibility.Hidden;
            label_fecha.Visibility = Visibility.Hidden;
            label_lugar.Visibility = Visibility.Hidden;
            txt_lugar_adopcion.Visibility = Visibility.Hidden;
            check_imagen.Visibility = Visibility.Visible;
            selec_img.Visibility = Visibility.Visible;

  
        }

        private void LimpiarControles()
        {
            //Veterinario
            txt_nombre_veterinaria.Text = "";
            txt_clinica.Text = "";
            txt_calle_veterinaria.Text = "";
            txt_localidad.Text = "";
            txt_telefono_clinica.Text = "";
            txt_telefono_urgencias.Text = "";
            txt_clinica_vet.Text = "";
            //Visitas veterinario
            txt_descripcion_visita.Text = "";
            txt_precio_visita.Text = "";
            txt_tratamiento.Text = "";
            txt_duracion.Text = "";
            //Mascota
            txt_nombre.Text = "";
            txt_fecha.Text = "";
            txt_edad.Text = "";
            txt_especie.Text = "";
            txt_raza.Text = "";
            txt_sexo.Text = "";
            txt_color.Text = "";
            txt_cartilla.Text = "";
            txt_chip.Text = "";
            txt_lugar_adopcion.Text = "";
            txt_fecha_adopcion.Text = "";
            ruta_imagen.Text = "";
            txt_id.Text = "";
            imagenMascota = null;
            check_imagen.IsChecked = false;
            adoptado.IsChecked = false;

            txt_fecha_adopcion.Visibility = Visibility.Hidden;
            label_fecha.Visibility = Visibility.Hidden;
            label_lugar.Visibility = Visibility.Hidden;
            txt_raza.Visibility = Visibility.Hidden;
            txt_sexo.Visibility = Visibility.Hidden;
            txt_especie.Visibility = Visibility.Hidden;
            txt_fecha.Visibility = Visibility.Hidden;
            label_fecha.Visibility = Visibility.Hidden;
            label_lugar.Visibility = Visibility.Hidden;
            txt_lugar_adopcion.Visibility = Visibility.Hidden;
            check_imagen.Visibility = Visibility.Visible;
            selec_img.Visibility = Visibility.Visible;
        }




        /*****************************************************************************************************************************/
        //METODOS PARA FORMATEAR LOS CAMPOS DE LA PANTALLA MASCOTAS
        /*****************************************************************************************************************************/


        //este Metodo calcula la edad de nuestra mascota  meses y años
        private void cargar_fecha_nac(object sender, RoutedEventArgs e)
        {
            //recojo mes y año actual
            String anoActual = DateTime.Today.ToString("yyyy");
            String mesActual = DateTime.Today.ToString("MM");

            //ESTO NO HACE FALTA, funciona si esto, no lo borro de momento por si acaso
            //  fecha.DisplayDate.Year.ToString();
            // fecha.DisplayDate.Month.ToString();

            //recojo mes y año nacimiento
            String mesNacimiento = fecha.DisplayDate.Month.ToString();
            String anoNacimiento = fecha.DisplayDate.Year.ToString();

            String fecha2 = fecha.SelectedDate.ToString();
            txt_fecha.Text = fecha2;



            //calcula tiempo transcurrido
            int anos = Int32.Parse(anoActual) - Int32.Parse(anoNacimiento);
            int meses = Int32.Parse(mesActual) - Int32.Parse(mesNacimiento);


            //Si solo tiene meses , muestra meses
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
                {

                    txt_edad.Text = anos.ToString() + " año y " + meses.ToString() + " meses.";
                }
            }

            //Si ya tiene un año o mas, muestra años y meses
            else
            {
                if (anos >= 2)
                {

                    txt_edad.Text = anos.ToString() + " años y " + meses.ToString() + "meses";
                }
                else
                {

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
                adoptadoCheck = "si";
                txt_lugar_adopcion.Visibility = Visibility.Visible;
                // txt_fecha_adopcion.Visibility = Visibility.Visible;
                dp_fecha_adopcion.Visibility = Visibility.Visible;
                label_lugar.Visibility = Visibility.Visible;
                label_fecha.Visibility = Visibility.Visible;
            }
            else
            {
                adoptadoCheck = "no";
                txt_lugar_adopcion.Visibility = Visibility.Hidden;
                txt_fecha_adopcion.Visibility = Visibility.Hidden;
                dp_fecha_adopcion.Visibility = Visibility.Hidden;
                label_lugar.Visibility = Visibility.Hidden;
                label_fecha.Visibility = Visibility.Hidden;
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
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
                {
                    // Set filter for file extension and default file extension 
                    DefaultExt = ".jpg",
                    Filter = "Todos (*.*)|*.*|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg"
                };

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
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
        private void cargarEspecies()
        {
            try
            {
                //abrimos la conexion
                con.Open();
            }
            catch (Exception ee)
            {
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

        //rellena el comboBox con las razas segun se haya elegido en el combo de especies
        private void cargaRazas(object sender, SelectionChangedEventArgs e)
        {

            ComboBox cb = (ComboBox)sender;

            DataRowView drv = (DataRowView)cb.SelectedItem;
            string cadena = drv.Row[0].ToString();
            cadena.ToLower();
            // MessageBox.Show(cadena);
            string cadena2 = drv.Row[1].ToString();
            txt_especie.Text = cadena2;

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

            switch (cadena)
            {

                case "1":
                    string queryP = "SELECT * FROM perro";
                    SqlDataAdapter miAdaptadorSqlP = new SqlDataAdapter(queryP, con);

                    using (miAdaptadorSqlP)
                    {
                        DataTable dtRazas = new DataTable();
                        miAdaptadorSqlP.Fill(dtRazas);

                        razas.DisplayMemberPath = "raza";
                        razas.SelectedValuePath = "id_perro";
                        razas.ItemsSource = dtRazas.DefaultView;

                    }
                    break;
                case "2":
                    string queryG = "SELECT * FROM gato";
                    SqlDataAdapter miAdaptadorSqlG = new SqlDataAdapter(queryG, con);

                    using (miAdaptadorSqlG)
                    {
                        DataTable dtRazas = new DataTable();
                        miAdaptadorSqlG.Fill(dtRazas);

                        razas.DisplayMemberPath = "raza";
                        razas.SelectedValuePath = "id_gato";
                        razas.ItemsSource = dtRazas.DefaultView;
                    }
                    break;
                case "3":
                    string queryA = "SELECT * FROM ave";
                    SqlDataAdapter miAdaptadorSqlA = new SqlDataAdapter(queryA, con);

                    using (miAdaptadorSqlA)
                    {
                        DataTable dtRazas = new DataTable();
                        miAdaptadorSqlA.Fill(dtRazas);

                        razas.DisplayMemberPath = "raza";
                        razas.SelectedValuePath = "id_ave";
                        razas.ItemsSource = dtRazas.DefaultView;
                    }
                    break;
                case "4":
                    string queryR = "SELECT * FROM roedor";
                    SqlDataAdapter miAdaptadorSqlR = new SqlDataAdapter(queryR, con);

                    using (miAdaptadorSqlR)
                    {
                        DataTable dtRazas = new DataTable();
                        miAdaptadorSqlR.Fill(dtRazas);

                        razas.DisplayMemberPath = "raza";
                        razas.SelectedValuePath = "id_roedor";
                        razas.ItemsSource = dtRazas.DefaultView;
                    }
                    break;
                case "5":
                    string queryPe = "SELECT * FROM pez";
                    SqlDataAdapter miAdaptadorSqlPe = new SqlDataAdapter(queryPe, con);

                    using (miAdaptadorSqlPe)
                    {
                        DataTable dtRazas = new DataTable();
                        miAdaptadorSqlPe.Fill(dtRazas);

                        razas.DisplayMemberPath = "raza";
                        razas.SelectedValuePath = "id_pez";
                        razas.ItemsSource = dtRazas.DefaultView;
                    }
                    break;
            }


            con.Close();
        }

        //si hay tratamiento visualiza text para rellenar
        private void tratamiento_check_clic(object sender, RoutedEventArgs e)
        {
            if (tratamiento_check.IsChecked == true)
            {
                tratamientoChek = "SI";
                txt_tratamiento.Visibility = Visibility.Visible;
                txt_duracion.Visibility = Visibility.Visible;
            }
            else
            {
                tratamientoChek = "NO";
                txt_tratamiento.Visibility = Visibility.Hidden;
                txt_duracion.Visibility = Visibility.Hidden;
            }
        }


        /*****************************************************************************************************************************/
        //MODIFICAR LAS PANTALLAS DE VISIBILITY Y HIDDEN
        /*****************************************************************************************************************************/

        private void añadir_mascota_Click(object sender, RoutedEventArgs e)
        {
            img_principal.Visibility = Visibility.Hidden;
            agregarMasc.Visibility = Visibility.Visible;


            if (veterinaria.Visibility == Visibility.Visible)
            {
                veterinaria.Visibility = Visibility.Hidden;
            }
        }

        private void abrir_veterinario(object sender, RoutedEventArgs e)
        {
            agregarMasc.Visibility = Visibility.Hidden;
            veterinaria.Visibility = Visibility.Visible;
            //perder_foco(sender, e);
        }


        /*****************************************************************************************************************************/
        //MODIFICAR IMAGENES SIN PROGRAMAR AUN
        /*****************************************************************************************************************************/

        private void diario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void galeria_Click(object sender, RoutedEventArgs e)
        {

        }



        /*****************************************************************************************************************************/
        //GENERAR PDFS
        /*****************************************************************************************************************************/

        private void PDF_mascota_Personal(object sender, RoutedEventArgs e)
        {
            nombre = txt_nombre.Text;
            fechaNac = txt_fecha.Text;
            edad = txt_edad.Text;
            raza = txt_raza.Text;
            sexo = txt_sexo.Text;
            especieAni = txt_especie.Text;
            color = txt_color.Text;
            cartilla = txt_cartilla.Text;
            numChip = txt_chip.Text;
            fechaAdop = txt_fecha_adopcion.Text;
            lugarAdop = txt_lugar_adopcion.Text;
            ruta = ruta_imagen.Text;  
            idMAsc = txt_id.Text;
            String buscar="\\";
            String reemplazo = "\\\\";
            String rataa= ruta.Replace(buscar,reemplazo);
           
            if(nombre=="" || edad==""|| raza == "" || color == ""){
                    MessageBox.Show("Los campos deben contener información");
                }else
                {
                    //crea documento
                    var PageSize = new iTextSharp.text.Rectangle(700f, 1024f);
                    FileStream fs = new FileStream("Mascota.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                    Document doc = new Document(PageSize);
                    iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();
               

                // propiedades titulo
                Phrase txtTitulo = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("           NOMBRE: " + nombre,
                                       FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                       new iTextSharp.text.BaseColor(64, 5, 56))));
                    Phrase fechaNaci = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("        FECHA NACIMIENTO: " + fechaNac,
                                       FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                       new iTextSharp.text.BaseColor(64, 5, 56))));
                    Phrase edadP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("        EDAD: " + edad,
                                      FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                      new iTextSharp.text.BaseColor(64, 5, 56))));
                    Phrase razaP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("           RAZA: " + raza,
                                      FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                      new iTextSharp.text.BaseColor(64, 5, 56))));
                     Phrase sexoP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("           SEXO: " + sexo,
                                     FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                     new iTextSharp.text.BaseColor(64, 5, 56))));
                     Phrase especieAniP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("       ESPECIE: " + especieAni,
                                     FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                     new iTextSharp.text.BaseColor(64, 5, 56))));
                     Phrase colorP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("           COLOR: " + color,
                                     FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                     new iTextSharp.text.BaseColor(64, 5, 56))));
                     Phrase cartillaP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("      NÚMERO CARTILLA: " + cartilla,
                                     FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                     new iTextSharp.text.BaseColor(64, 5, 56))));
                      Phrase fechaAdopP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("        FECHA ADOPCIÓN: " + fechaAdop,
                                     FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                     new iTextSharp.text.BaseColor(64, 5, 56))));
                      Phrase lugarAdopP = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("         LUGAR ADOPCIÓN: " + lugarAdop,
                                     FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                                     new iTextSharp.text.BaseColor(64, 5, 56))));
                //añade los datos al  documento
                    doc.Add(new iTextSharp.text.Paragraph(txtTitulo));
                    doc.Add(new iTextSharp.text.Paragraph(fechaNaci));
                    doc.Add(new iTextSharp.text.Paragraph(edadP));
                    doc.Add(new iTextSharp.text.Paragraph(especieAniP));
                    doc.Add(new iTextSharp.text.Paragraph(razaP));
                    doc.Add(new iTextSharp.text.Paragraph(sexoP));
                    doc.Add(new iTextSharp.text.Paragraph(colorP));
                    doc.Add(new iTextSharp.text.Paragraph(cartillaP));
                    doc.Add(new iTextSharp.text.Paragraph(fechaAdopP));
                    doc.Add(new iTextSharp.text.Paragraph(lugarAdopP));
                
                // propiedades imagen logo 1
                var posX = 200f;
                var posY = 100f;
                String ruta1 = "huella.png";
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(ruta1, UriKind.Relative);
                bmp.EndInit();

                //ejecuta la ruta absoluta donde esta el ejecutable AQUI ES DONDE COLOCAMOS LA CARPETA IMAGENES QUE USAMOS EN ESTA APL
                string path = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "Imagenes" + System.IO.Path.DirectorySeparatorChar + ruta1;       //     
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(path);

                imagen.SetAbsolutePosition(posX, posY);
                imagen.ScaleAbsoluteWidth(80f); //  Escaral el tamaño de la imagen
                imagen.ScaleAbsoluteHeight(80f);

                doc.Add(imagen);

                // propiedades imagen logo 2
                var posX2 = 100f;
                var posY2 = 100f;
                String ruta2 = "huella.png";
                BitmapImage bmp2 = new BitmapImage();
                bmp2.BeginInit();
                bmp2.UriSource = new Uri(ruta1, UriKind.Relative);
                bmp2.EndInit();

                //ejecuta la ruta absoluta donde esta el ejecutable AQUI ES DONDE COLOCAMOS LA CARPETA IMAGENES QUE USAMOS EN ESTA APL
                string path2 = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "Imagenes" + System.IO.Path.DirectorySeparatorChar + ruta1;       //     
                iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(path2);

                imagen2.SetAbsolutePosition(posX2, posY2);
                imagen2.ScaleAbsoluteWidth(80f); //  Escaral el tamaño de la imagen
                imagen2.ScaleAbsoluteHeight(80f);
                //  añadimos la imagen al documneto
                doc.Add(imagen2);

                // propiedades imagen logo 3
                var posX3 = 300f;
                var posY3 = 100f;
                String ruta3 = "huella.png";
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.UriSource = new Uri(ruta3, UriKind.Relative);
                bmp3.EndInit();

                //ejecuta la ruta absoluta donde esta el ejecutable AQUI ES DONDE COLOCAMOS LA CARPETA IMAGENES QUE USAMOS EN ESTA APL
                string path3 = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "Imagenes" + System.IO.Path.DirectorySeparatorChar + ruta1;       //     
                iTextSharp.text.Image imagen3 = iTextSharp.text.Image.GetInstance(path2);

                imagen3.SetAbsolutePosition(posX3, posY3);
                imagen3.ScaleAbsoluteWidth(80f); //  Escaral el tamaño de la imagen
                imagen3.ScaleAbsoluteHeight(80f);
                //  añadimos la imagen al documneto
                doc.Add(imagen3);
                
                // propiedades imagen logo 4
                var posX4 = 400f;
                var posY4 = 100f;
                String ruta4 = "huella.png";
                BitmapImage bmp4 = new BitmapImage();
                bmp4.BeginInit();
                bmp4.UriSource = new Uri(ruta4, UriKind.Relative);
                bmp4.EndInit();

                //ejecuta la ruta absoluta donde esta el ejecutable AQUI ES DONDE COLOCAMOS LA CARPETA IMAGENES QUE USAMOS EN ESTA APL
                string path4 = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "Imagenes" + System.IO.Path.DirectorySeparatorChar + ruta1;       //     
                iTextSharp.text.Image imagen4 = iTextSharp.text.Image.GetInstance(path4);

                imagen4.SetAbsolutePosition(posX4, posY4);
                imagen4.ScaleAbsoluteWidth(80f); //  Escaral el tamaño de la imagen
                imagen.ScaleAbsoluteHeight(80f);
                //  añadimos la imagen al documneto
                doc.Add(imagen4);


                // propiedades imagen
                var posXl = 200f;
                 var posYl = 300f;

                 iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(rataa);
                 image.SetAbsolutePosition(posXl, posYl);
                // image.ScaleAbsoluteWidth(60f); //  Escalar el tamaño de la imagen
                //image.ScaleAbsoluteHeight(60f);
                 doc.Add(image); //añade imagen al documento



                // escribe pie de pagina
                writer.PageEvent = new PiePagina();

                    doc.Close();


                    var p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(@"Mascota.pdf") { UseShellExecute = true };
                    p.Start();

                }
        }
    

        private void PDF_mascota(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                //  MessageBox.Show("Conectado a la base de datos.\n Muestra datos de la tabla VETERINARIO");
                DataTable dataTabla = new DataTable();
                String cadena = "Select * from mascota";
                SqlDataAdapter adp = new SqlDataAdapter(cadena, con);
                adp.Fill(dataTabla);
                string condicion = "id_mascota>0";
                filas = dataTabla.Select(condicion);
                var posicion = filas.Length - 1;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                MessageBox.Show(ee.StackTrace);
            }

            //crea documento
            var PageSize = new iTextSharp.text.Rectangle(1024f,700f );
            FileStream fs = new FileStream("Mascotas.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize);
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            // doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            doc.Open();

            // propiedades titulo
            Phrase txtTitulo = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("MIS MASCOTAS",
                               FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));

            //añade el titulo a documento
            doc.Add(new iTextSharp.text.Paragraph(txtTitulo));

            // propiedades imagen
            var posX = 900f;
            var posY = 610f;


            String ruta1 = "huella.png";
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(ruta1, UriKind.Relative);
            bmp.EndInit();


            //ejecuta la ruta absoluta donde esta el ejecutable AQUI ES DONDE COLOCAMOS LA CARPETA IMAGENES QUE USAMOS EN ESTA APL
            string path = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "Imagenes" + System.IO.Path.DirectorySeparatorChar + ruta1;       //     
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(path);

            imagen.SetAbsolutePosition(posX, posY);
            imagen.ScaleAbsoluteWidth(80f); //  Escaral el tamaño de la imagen
            imagen.ScaleAbsoluteHeight(80f);

            //  añadimos la imagen al documneto
            doc.Add(imagen);

            // escribe pie de pagina
            writer.PageEvent = new PiePagina();

            PdfPTable cabecera = new PdfPTable(12);
            //propiedades de la cabecera de la tabla
            cabecera.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            cabecera.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            cabecera.DefaultCell.BorderColor = new BaseColor(0, 0, 0); //color negro
            cabecera.DefaultCell.BorderWidth = 1;
            cabecera.DefaultCell.BackgroundColor = new BaseColor(60, 161, 245);
            cabecera.DefaultCell.MinimumHeight = 20f;

            //tamaño columnas
            float[] arrayTamañoColumnas = new float[] { 60f, 90f, 80f, 100f, 80f, 80f, 120f, 80f,60f, 80f, 80f, 80f };

            //asignamos el array de anchos de columna a la tabla
            cabecera.SetTotalWidth(arrayTamañoColumnas);

            //estilo de cabecera
            Phrase nombre = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk ("Nombre", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase fecha_nacimiento = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Fecha Nac ", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase edad = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Edad", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase especie = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Especie", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase raza = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Raza", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase sexo = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Sexo", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                              new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase color = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Color", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                              new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase num_cartilla_sanitaria = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Núm Chip", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                             new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase num_chip = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Núm Cartilla", FontFactory.GetFont(FontFactory.COURIER, 10f, Font.BOLD,
                             new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase adoptado = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Adoptado", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                             new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase fecha_adopcion = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Fecha Adopción", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                             new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase lugar_adopcion = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Lugar Adopción", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                             new iTextSharp.text.BaseColor(64, 5, 56))));
            //Phrase imagenMasc = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Imagen", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                       //      new iTextSharp.text.BaseColor(64, 5, 56))));


            //añade cabecera a la tabla
            cabecera.AddCell(nombre);
            cabecera.AddCell(fecha_nacimiento);
            cabecera.AddCell(edad);
            cabecera.AddCell(especie);
            cabecera.AddCell(raza);
            cabecera.AddCell(sexo);
            cabecera.AddCell(color);
            cabecera.AddCell(num_cartilla_sanitaria);
            cabecera.AddCell(num_chip);
            cabecera.AddCell(adoptado);
            cabecera.AddCell(fecha_adopcion);
            cabecera.AddCell(lugar_adopcion);
           // cabecera.AddCell(imagenMasc);

            cabecera.WriteSelectedRows(0, 10, 20f, 600, writer.DirectContent);

            //Propiedades de la tabla con datos, igual q de la cabecera
            PdfPTable datos = new PdfPTable(12);
            datos.SetTotalWidth(arrayTamañoColumnas);
            datos.DefaultCell.MinimumHeight = 40f;
            datos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            datos.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            int cf = 0;
            foreach (DataRow registro in filas)
            {

                datos.AddCell(registro["nombre"] + " ");
                datos.AddCell(registro["fecha_nacimiento"] + " ");
                datos.AddCell(registro["edad"] + " ");
                datos.AddCell(registro["especie"] + " ");
                datos.AddCell(registro["raza"] + " ");
                datos.AddCell(registro["sexo"] + " ");
                datos.AddCell(registro["color"] + " ");
                datos.AddCell(registro["num_cartilla_sanitaria"] + " ");
                datos.AddCell(registro["num_chip"] + " ");
                datos.AddCell(registro["adoptado"] + " ");
                datos.AddCell(registro["fecha_adopcion"] + " ");
                datos.AddCell(registro["lugar_adopcion"] + " ");
                
                //iTextSharp.text.Image imagenP = iTextSharp.text.Image.GetInstance(registro["imagen"] + " ");
              //  datos.AddCell(imagenP);
                cf++;
                if (cf == 9)
                {
                    //salto de pagina
                    datos.WriteSelectedRows(0, -1, 20f,565f, writer.DirectContent);
                    //borrar datos de la PdfTable
                    datos = new PdfPTable(12);
                    datos.SetTotalWidth(arrayTamañoColumnas);
                    datos.DefaultCell.MinimumHeight = 40f;
                    datos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //nueva pagina
                    doc.NewPage();
                    doc.Add(new iTextSharp.text.Paragraph(txtTitulo));
                    doc.Add(imagen);
                    cabecera.WriteSelectedRows(0, -1, 20f, 600f, writer.DirectContent);
                    cf = 0;
                }
            }

            datos.WriteSelectedRows(0, -1, 20f, 565, writer.DirectContent);
            doc.Close();


            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(@"Mascotas.pdf") { UseShellExecute = true };
            p.Start();


        }

        //PDF de la tabla vetetrinario
        private void PDF_veterinario(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                //  MessageBox.Show("Conectado a la base de datos.\n Muestra datos de la tabla VETERINARIO");
                DataTable dataTabla = new DataTable();
                String cadena = "Select m.nombre,v.id_veterinario, v.clinica, v.calle, v.localidad, v.nombre_veterinario, v.telefono, v.telefono_urgencias from veterinario v, mascota m WHERE m.id_mascota = v.id_mascota";
                SqlDataAdapter adp = new SqlDataAdapter(cadena, con);
                adp.Fill(dataTabla);
                string condicion = "id_veterinario>0";
                filas = dataTabla.Select(condicion);
                var posicion = filas.Length - 1;

            }
            catch (Exception ee) { MessageBox.Show(ee.Message);
                MessageBox.Show(ee.StackTrace);
            }

            //crea documento
            var PageSize = new iTextSharp.text.Rectangle(700f, 1024f);
            FileStream fs = new FileStream("Veterinario.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize);
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            // doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            doc.Open();

            // propiedades titulo
            Phrase txtTitulo = new iTextSharp.text.Phrase(40f, new iTextSharp.text.Chunk("Listado Veterinario",
                               FontFactory.GetFont(FontFactory.COURIER_OBLIQUE, 26f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));

            //añade el titulo a documento
            doc.Add(new iTextSharp.text.Paragraph(txtTitulo));

            // propiedades imagen
            var posX = 550f;
            var posY = 920f;


            String ruta1 = "veterinaria.jpg";
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(ruta1, UriKind.Relative);
            bmp.EndInit();


            //ejecuta la ruta absoluta donde esta el ejecutable AQUI ES DONDE COLOCAMOS LA CARPETA IMAGENES QUE USAMOS EN ESTA APL
            string path = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "Imagenes" + System.IO.Path.DirectorySeparatorChar + ruta1;       //     
            
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(path);
           

            imagen.SetAbsolutePosition(posX, posY);
            imagen.ScaleAbsoluteWidth(80f); //  Escaral el tamaño de la imagen
            imagen.ScaleAbsoluteHeight(80f);

            //  añadimos la imagen al documneto
            doc.Add(imagen);
            
            // escribe pie de pagina
            writer.PageEvent = new PiePagina();

            PdfPTable cabecera = new PdfPTable(7);
            //propiedades de la cabecera de la tabla
            cabecera.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            cabecera.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            cabecera.DefaultCell.BorderColor = new BaseColor(0, 0, 0); //color negro
            cabecera.DefaultCell.BorderWidth = 1;
            cabecera.DefaultCell.BackgroundColor = new BaseColor(60, 161, 245);
            cabecera.DefaultCell.MinimumHeight = 20f;



            //tamaño columnas
            float[] arrayTamañoColumnas = new float[] { 90f, 80f, 100f, 80f,80f, 120f, 80f };

            //asignamos el array de anchos de columna a la tabla
            cabecera.SetTotalWidth(arrayTamañoColumnas);

            //estilo de cabecera
           Phrase clinica = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Clínica ", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase calle = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Calle", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase localidad = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Localidad", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase nombre_vet = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Nombre vet", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                               new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase telefono = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Teléfono", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                              new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase telefono_urgencias = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Teléfono Urgencias", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                              new iTextSharp.text.BaseColor(64, 5, 56))));
            Phrase cadNombre = new iTextSharp.text.Phrase(25f, new iTextSharp.text.Chunk("Nombre Mascota", FontFactory.GetFont(FontFactory.COURIER, 14f, Font.BOLD,
                             new iTextSharp.text.BaseColor(64, 5, 56))));


            //añade cabecera a la tabla
            cabecera.AddCell(clinica);
            cabecera.AddCell(calle);
            cabecera.AddCell(localidad);
            cabecera.AddCell(nombre_vet);
            cabecera.AddCell(telefono);
            cabecera.AddCell(telefono_urgencias);
            cabecera.AddCell(cadNombre);

            cabecera.WriteSelectedRows(0, 10, 20f, 920, writer.DirectContent);

            //Propiedades de la tabla con datos, igual q de la cabecera
            PdfPTable datos = new PdfPTable(7);
            datos.SetTotalWidth(arrayTamañoColumnas);
            datos.DefaultCell.MinimumHeight = 40f;
            datos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            datos.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            int cf = 0;
            foreach (DataRow registro in filas)
            {
           
                
                datos.AddCell(registro["clinica"] + " ");
                datos.AddCell(registro["calle"] + " ");
                datos.AddCell(registro["localidad"] + " ");
                datos.AddCell(registro["nombre_veterinario"] + " ");
                datos.AddCell(registro["telefono"] + " ");
                datos.AddCell(registro["telefono_urgencias"] + " ");
                datos.AddCell(registro["nombre"] + " ");
              

                cf++;
                if (cf == 20)
                {
                    //salto de pagina
                    datos.WriteSelectedRows(0, -1, 20f, 900f, writer.DirectContent);
                    //borrar datos de la PdfTable
                    datos = new PdfPTable(3);
                    datos.SetTotalWidth(arrayTamañoColumnas);
                    datos.DefaultCell.MinimumHeight = 40f;
                    datos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //nueva pagina
                    doc.NewPage();
                    doc.Add(new iTextSharp.text.Paragraph(txtTitulo));
                    doc.Add(imagen);
                    cabecera.WriteSelectedRows(0, -1, 20f, 920f, writer.DirectContent);
                    cf = 0;
                }
            }

            datos.WriteSelectedRows(0, -1, 20f, 888f, writer.DirectContent);
            doc.Close();


            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(@"Veterinario.pdf") { UseShellExecute = true };
            p.Start();


        }

        //Footer PDF Veterinaria
        public partial class PiePagina : PdfPageEventHelper
        {
            private int numeroDePagina = 0;

            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                numeroDePagina++;

                iTextSharp.text.Paragraph footer = new iTextSharp.text.Paragraph("Autor@s:ALLENDE EGUÍA DEL VALLE & AIDA VICENTE MARTÍN", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.ITALIC));
                iTextSharp.text.Paragraph linea2 = new iTextSharp.text.Paragraph("NºPag.:" + numeroDePagina, FontFactory.GetFont(FontFactory.TIMES, 8, iTextSharp.text.Font.BOLD));

                footer.Alignment = Element.ALIGN_RIGHT;

                PdfPTable footerTbl = new PdfPTable(2);

                footerTbl.TotalWidth = 700;

                footerTbl.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell cell = new PdfPCell(footer);

                cell.Border = 0;

                cell.PaddingLeft = 10;

                footerTbl.AddCell(cell);

                cell = new PdfPCell(linea2);
                cell.Border = 0;

                cell.PaddingLeft = 10;
                footerTbl.AddCell(cell);
                footerTbl.WriteSelectedRows(0, -1, 115, 30, writer.DirectContent);
            }
        }
    }
}

