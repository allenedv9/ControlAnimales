using System;
using System.Collections.Generic;
using System.Text;

namespace ControlAnimales
{
    class Mascota
    {

        private int id_mascota;
        private string nombre;
        private string fecha_nacimineto;
        private string edad;
        private string especie;
        private string raza;
        private string sexo;
        private string color;
        private string num_cartilla_sanitaria;
        private string num_chip;
        private string adoptado;
        private string fecha_adopcion;
        private string lugar_adopcion;
        private string imagen;

        public int Id_mascota { get => id_mascota; set => id_mascota = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Fecha_nacimineto { get => fecha_nacimineto; set => fecha_nacimineto = value; }
        public string Edad { get => edad; set => edad = value; }
        public string Especie { get => especie; set => especie = value; }
        public string Raza { get => raza; set => raza = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Color { get => color; set => color = value; }
        public string Num_cartilla_sanitaria { get => num_cartilla_sanitaria; set => num_cartilla_sanitaria = value; }
        public string Num_chip { get => num_chip; set => num_chip = value; }
        public string Adoptado { get => adoptado; set => adoptado = value; }
        public string Fecha_adopcion { get => fecha_adopcion; set => fecha_adopcion = value; }
        public string Lugar_adopcion { get => lugar_adopcion; set => lugar_adopcion = value; }
        public string Imagen { get => imagen; set => imagen = value; }
    }
}
