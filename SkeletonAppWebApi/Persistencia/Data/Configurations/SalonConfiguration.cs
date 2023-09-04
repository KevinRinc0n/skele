using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder.ToTable("Salon");

        builder.Property(s => s.Id)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(s => s.NombreSalon)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(s => s.Capacidad)
        .IsRequired()
        .HasColumnType("int");
    }
}