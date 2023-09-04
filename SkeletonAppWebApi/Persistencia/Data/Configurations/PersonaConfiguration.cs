using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.Property(p => p.Id)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(p => p.NombrePersona)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.ApellidoPersona)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.DireccionPersona)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.Matriculas) 
        .IsRequired();

        builder.Property(p => p.TrainerSalones)
        .IsRequired();

        builder.HasOne(p => p.Genero)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdGeneroFk);

        builder.HasOne(p => p.Ciudad)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdCiudadFk);

        builder.HasOne(p => p.TipoPersona)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdTipoPersonaFk);
    }
}
