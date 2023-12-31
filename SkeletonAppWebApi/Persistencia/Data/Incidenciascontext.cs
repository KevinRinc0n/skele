using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public class Incidenciascontext : DbContext
{
    public Incidenciascontext(DbContextOptions<Incidenciascontext> options) : base(options)
    {

    }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Salon> Salones { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<TipoPersona> TipoPersonas { get; set; }
    public DbSet<TrainerSalon> TrainerSalones { get; set; }
    public DbSet<Departamento> Departamentos { get; set; } 
    public DbSet<Pais> Paises { get; set; } 
    public DbSet<Genero> Generos { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainerSalon>().HasKey(ps => new { ps.IdPersonaFk, ps.IdSalonFk });
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
