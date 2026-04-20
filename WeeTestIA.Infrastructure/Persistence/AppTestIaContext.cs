using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WeeTestIA.Domain;

namespace WeeTestIA.Infrastructure.Persistence;

public partial class AppTestIaContext : DbContext
{
    public AppTestIaContext()
    {
    }

    public AppTestIaContext(DbContextOptions<AppTestIaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatPerfil> CatPerfils { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=AppTestIA;User ID=UserAppNode;Password=UserAppNode.$549;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatPerfil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Id_CatPerfil");

            entity.ToTable("CatPerfil");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualización).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
