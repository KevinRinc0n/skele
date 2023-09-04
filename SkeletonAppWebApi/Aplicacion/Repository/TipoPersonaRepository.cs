using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Persistencia.Repository;

public class TipoPersonaRepository : GenericRepo<TipoPersona>, ITipoPersona
{
    private readonly Incidenciascontext _context;
    public TipoPersonaRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoPersona>> GetAllAsync()
    {
        return await _context.TipoPersonas
            .Include(p => p.Personas)
            .ToListAsync();
    }

    public override async Task<TipoPersona> GetByIdAsync(int id)  
    {
        return await _context.TipoPersonas 
        .Include(p => p.Personas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
} 