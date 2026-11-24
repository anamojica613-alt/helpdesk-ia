using HelpDeskIA.Api.Models;
using HelpDeskIA.Api.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskIA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly HelpDeskIaContext _db;
    private readonly IAsistenteTickets _asistente;

    public TicketsController(HelpDeskIaContext db, IAsistenteTickets asistente)
    {
        _db = db;
        _asistente = asistente;
    }

    public record NuevoTicket(string Titulo, string Descripcion, int? CategoriaId, int SolicitanteId);

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var tickets = await _db.Tickets
            .OrderByDescending(t => t.FechaCreacion)
            .Select(t => new
            {
                t.Id,
                t.Titulo,
                t.Descripcion,
                t.Prioridad,
                t.Estado,
                t.CategoriaId,
                t.SolicitanteId,
                t.SugerenciaIa,
                t.FechaCreacion
            })
            .ToListAsync();

        return Ok(tickets);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(NuevoTicket datos)
    {
        var analisis = await _asistente.AnalizarAsync(datos.Titulo, datos.Descripcion);

        var ticket = new Ticket
        {
            Titulo = datos.Titulo,
            Descripcion = datos.Descripcion,
            CategoriaId = datos.CategoriaId ?? analisis.CategoriaId,
            SolicitanteId = datos.SolicitanteId,
            Prioridad = analisis.Prioridad,
            Estado = "Abierto",
            SugerenciaIa = analisis.Sugerencia
        };

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Listar), new { id = ticket.Id }, new
        {
            ticket.Id,
            ticket.CategoriaId,
            ticket.Prioridad,
            ticket.SugerenciaIa
        });
    }
}