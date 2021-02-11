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

        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        
        
public string Nombre { get => nombre; set => nombre = value; }
public string Clinica { get => clinica; set => clinica = value; }
public string Calle { get => calle; set => calle = value; }
public string Telefono { get => telefono; set => telefono = value; }
public string Telefono_urgencias { get => telefono_urgencias; set => telefono_urgencias = value; }




public string this[string path]
{
get
{
string result = null;

if (path == "Nombre"){
if (this.nombre < 0 || this.id > 9999)
{
result = "Id incorrecto.";
}
}

if (path == "Telefono" && this.telefono != null && this.telefono != "" && this.telefono != "Teléfono Clínica")
{
Regex Expresión = new Regex(@"^[0-9]{3} [0-9]{3} [0-9]{3}$");
//  Regex Expresión = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

Console.WriteLine("Telefono :" + this.telefono);
Console.WriteLine(Expresión.IsMatch(this.telefono));
//  return regex.IsMatch(s);

if (!Expresión.IsMatch(this.telefono))
{
result = "Telefono Incorrecto. Formato correcto(999 999 999)";
}
}

                if (path == "Telefono_urgencias" || this.telefono_urgencias != null && this.telefono_urgencias != "" && this.telefono_urgencias != "Teléfono Urgencias")
                {
                    Regex Expresión = new Regex(@"^[0-9]{3} [0-9]{3} [0-9]{3}$");

}

                    if (!Expresión.IsMatch(this.telefono_urgencias))
                    {
                        result = "Telefono Incorrecto. Formato correcto(999 999 999)";
                    }
 
              }
                return result;

}*/
    }
}