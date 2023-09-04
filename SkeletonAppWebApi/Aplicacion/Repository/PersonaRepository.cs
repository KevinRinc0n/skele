using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Aplicacion.Repository;

public class PersonaRepository : GenericRepo<Persona>, IPersona
{
    protected readonly Incidenciascontext _context;

    public PersonaRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    } 

    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas
        .Include(p => p.Matriculas)
        .ToListAsync();
    }

    public override async Task<Persona> GetByIdAsync(int id) 
    {
        return await _context.Personas
        .Include(p => p.Matriculas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
