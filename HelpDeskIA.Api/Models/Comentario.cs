using System;
using System.Collections.Generic;

namespace HelpDeskIA.Api.Models;

public partial class Comentario
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public int UsuarioId { get; set; }

    public string Texto { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
