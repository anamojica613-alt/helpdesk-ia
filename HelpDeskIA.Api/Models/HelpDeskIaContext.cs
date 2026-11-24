using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskIA.Api.Models;

public partial class HelpDeskIaContext : DbContext
{
    public HelpDeskIaContext()
    {
    }

    public HelpDeskIaContext(DbContextOptions<HelpDeskIaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07E5DFE6C1");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCFDCFA6509").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(60);
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC07EE8F67FA");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__Ticke__5AEE82B9");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__Usuar__5BE2A6F2");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC07E8E43045");

            entity.Property(e => e.AsignadoAid).HasColumnName("AsignadoAId");
            entity.Property(e => e.Estado)
                .HasMaxLength(15)
                .HasDefaultValue("Abierto");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(10)
                .HasDefaultValue("Media");
            entity.Property(e => e.SugerenciaIa).HasColumnName("SugerenciaIA");
            entity.Property(e => e.Titulo).HasMaxLength(150);

            entity.HasOne(d => d.AsignadoA).WithMany(p => p.TicketAsignadoAs)
                .HasForeignKey(d => d.AsignadoAid)
                .HasConstraintName("FK__Tickets__Asignad__571DF1D5");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__Tickets__Categor__5165187F");

            entity.HasOne(d => d.Solicitante).WithMany(p => p.TicketSolicitantes)
                .HasForeignKey(d => d.SolicitanteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__Solicit__5629CD9C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07876A5E9D");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A191961D73D").IsUnique();

            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
