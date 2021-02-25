using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace ControlAnimales
{
    class Veterinaria : IDataErrorInfo
    {
        //campos del formulario Veterinaria
        String nombre ="";
        String clinica = "";
        String calle = "";
        String telefono = "";
        String telefono_urgencias = "";
        String localidad = "";

        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");



        public string Nombre { get => nombre; set => nombre = value; }
        public string Clinica { get => clinica; set => clinica = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Telefono_urgencias { get => telefono_urgencias; set => telefono_urgencias = value; }
        public string Localidad { get => localidad; set => localidad = value; }

        //metodos obligatorios de implementar al usar herencia :IDataErrorInfo
        public string Error
        {
            get
            {
                return null;
            }
        }
         //Comprueba que el dato introducido por usuario sea valido 
        public string this[string path]{
            get{
                string result = null;

                // Casilla Nombre 
                //Admite de 3 a 50 caracteres de texto
                if (path == "Nombre" ) {
                     
                    Regex regex = new Regex(@"^[a-zA-Z  _0-9,.-_#$%&'()*+,\-./:;<=>?@[\\\]^_`{|}~]{3,50}$");
                        if (!regex.IsMatch(this.Nombre)){
                            result = " admite carácteres alfabéticos y numeros, min 3 max 50";
                        }
                    return result;
                }
                // Localidad -- Admite de 3 a 50 caracteres de texto
                if (path == "Localidad")
                {
                    {
                        Regex regex = new Regex(@"^[a-zA-Z  _0-9,.-_#$%&'()*+,\-./:;<=>?@[\\\]^_`{|}~]{3,50}$");
                        if (!regex.IsMatch(this.Clinica))
                        {
                            result = " admite carácteres alfabéticos y numeros, min 3 max 50";

                        }

                        return result;
                    }
                }

                // Clinica -- Admite de 3 a 50 caracteres de texto
                if (path == "Clinica")
                {
                    {
                        Regex regex = new Regex(@"^[a-zA-Z  _0-9,.-_#$%&'()*+,\-./:;<=>?@[\\\]^_`{|}~]{3,50}$");
                        if (!regex.IsMatch(this.Clinica))
                        {
                            result = " admite carácteres alfabéticos y numeros, min 3 max 50";

                        }

                        return result;
                    }
                }
                // Calle -- Admite de 3 a 50 caracteres de texto
                  if (path == "Calle" )
                {
                    Regex regex = new Regex(@"^[a-zA-Z  _0-9,.-_#$%&'()*+,\-./:;<=>?@[\\\]^_`{|}~]{3,50}$");
                    if (!regex.IsMatch(this.Calle))
                    {
                        result = " admite carácteres alfabéticos y numeros, min 3 max 50";
                        
                    }
                   
                    return result;
                  
                }
                // Telefono -- Admite 9 digitos numericos
                if (path == "Telefono" )
                {
                    Regex Expresión = new Regex(@"^[0-9]{3}[0-9]{3}[0-9]{3}$");

                    if (!Expresión.IsMatch(this.telefono))
                    {
                        result = "Telefono Incorrecto. Formato correcto(999999999)";
                    
                    }
                  
                    return result;

                }
                // Telefono_urgencias -- Admite 9 digitos numericos
                 if (path == "Telefono_urgencias" )
                {
                    Regex Expresión = new Regex(@"^[0-9]{3}[0-9]{3}[0-9]{3}$");

                    if (!Expresión.IsMatch(this.telefono_urgencias))
                    {
                        result = "Telefono Incorrecto. Formato correcto(999999999)";
                    }
                 
                    return result;

                }
                
               return result;
            }
        }
    }
}