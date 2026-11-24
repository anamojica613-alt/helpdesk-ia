using System;
using System.Collections.Generic;

namespace HelpDeskIA.Api.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Ticket> TicketAsignadoAs { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketSolicitantes { get; set; } = new List<Ticket>();
}
