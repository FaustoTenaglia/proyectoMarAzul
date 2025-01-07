using System.Text.RegularExpressions;

namespace NeoRemiseria.Services;

public static class Helpers {
    // Recordar que las clases estáticas no requieren instanciacion y sus miembros pueden ser accedidos
    // directamente a través del nombre de la clase
    public static string FormatearPatente(string patente, string separador=" "){
        // Recordar que un método estático significa que pertenece a la clase en si y 
        // no a una instancia de la clase

        // Expresion regular para encontrar secuencias de letras o digitos
        var patron = new Regex(@"([a-zA-Z]+)|(\d+)");

        // Aplicar la expresion regular a la cadena de entrada y obtener las coincidencias
        var coincidencias = patron.Matches(patente);

        // Construir la cadena de salida concatenando las coincidencias separadas segun el separador
        return string.Join(separador, coincidencias.Cast<Match>().Select(m => m.Value.ToUpper()));
    }
}