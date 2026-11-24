namespace HelpDeskIA.Api.Servicios;

public class AsistenteReglas : IAsistenteTickets
{
    public Task<AnalisisTicket> AnalizarAsync(string titulo, string descripcion)
    {
        var texto = $"{titulo} {descripcion}".ToLowerInvariant();

        int? categoria = null;
        var sugerencia = "Pedir más detalles al usuario: qué hacía, desde cuándo ocurre y si aparece algún mensaje de error.";

        if (Contiene(texto, "correo", "outlook", "email"))
        {
            categoria = 5;
            sugerencia = "Revisar la configuración de la cuenta de correo, probar el acceso desde el webmail y reiniciar Outlook en modo seguro.";
        }
        else if (Contiene(texto, "contraseña", "clave", "bloqueado", "acceso", "login"))
        {
            categoria = 4;
            sugerencia = "Verificar si la cuenta está bloqueada y restablecer la contraseña siguiendo el procedimiento de identidad.";
        }
        else if (Contiene(texto, "internet", "wifi", "vpn", "conexión", "conexion"))
        {
            categoria = 3;
            sugerencia = "Comprobar el cable o la señal wifi, reiniciar el adaptador de red y probar con otro equipo.";
        }
        else if (Contiene(texto, "pantalla", "teclado", "mouse", "impresora", "enciende", "equipo"))
        {
            categoria = 1;
            sugerencia = "Revisar conexiones y alimentación del equipo, y probar el periférico en otro puerto o equipo.";
        }
        else if (Contiene(texto, "programa", "aplicación", "aplicacion", "instalar", "excel", "word"))
        {
            categoria = 2;
            sugerencia = "Cerrar y volver a abrir la aplicación, revisar actualizaciones pendientes y reinstalar si persiste.";
        }

        var prioridad =
            Contiene(texto, "urgente", "no puedo trabajar", "caído", "caido", "todos", "producción", "produccion") ? "Alta" :
            Contiene(texto, "lento", "a veces", "molesto") ? "Baja" : "Media";

        return Task.FromResult(new AnalisisTicket(categoria, prioridad, sugerencia));
    }

    private static bool Contiene(string texto, params string[] palabras)
        => palabras.Any(texto.Contains);
}