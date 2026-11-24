namespace HelpDeskIA.Api.Servicios;

public record AnalisisTicket(int? CategoriaId, string Prioridad, string Sugerencia);

public interface IAsistenteTickets
{
    Task<AnalisisTicket> AnalizarAsync(string titulo, string descripcion);
}