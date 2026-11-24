using System;
using System.Collections.Generic;

namespace HelpDeskIA.Api.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int? CategoriaId { get; set; }

    public string Prioridad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int SolicitanteId { get; set; }

    public int? AsignadoAid { get; set; }

    public string? SugerenciaIa { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaCierre { get; set; }

    public virtual Usuario? AsignadoA { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Usuario Solicitante { get; set; } = null!;
}
