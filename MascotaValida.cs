using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace ControlAnimales
{
    class MascotaValida : IDataErrorInfo
    {

        //campos del formulario Veterinaria
        String num_chip = "";
        String num_cartilla = "";


        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        public string Num_chip { get => num_chip; set => num_chip = value; }
        public string Num_cartilla { get => num_cartilla; set => num_cartilla = value; }

        public string Error
        {
            get
            {
                return null;
            }
        }
        //Comprueba que el dato introducido por usuario sea valido 
        public string this[string path]
        {
            get
            {
                string result = null;

                // Casilla Nombre 
                //Admite de 3 a 50 caracteres de texto
                if (path == "Num_chip")
                {
                    Regex Expresión = new Regex(@"^[0-9]{15}$");

                    if (!Expresión.IsMatch(this.Num_chip))
                    {
                        result = "Núm chip Incorrecto. 15 digitos numéricos";
                        return result;
                    }
                }
                if (path == "Num_cartilla")
                {

                    Regex regex = new Regex(@"^[a-z A_Z]{2}[0-9]{10}$");
                    if (!regex.IsMatch(this.Num_cartilla))
                    {
                        result = "Núm cartilla Incorrecto. dos letras y 10  digitos numéricos";
                        return result;
                    }

                }
                return result;
            }
        }

    }

    
}