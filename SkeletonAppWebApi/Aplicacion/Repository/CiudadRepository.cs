using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Persistencia.Repository;

public class CiudadRepository : GenericRepo<Ciudad>, ICiudad
{
    private readonly Incidenciascontext _context;
    public CiudadRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Ciudad>> GetAllAsync()
    {
        return await _context.Ciudades 
            .Include(p => p.Personas)
            .ToListAsync();
    }

    public override async Task<Ciudad> GetByIdAsync(int id)
    {
        return await _context.Ciudades
        .Include(p => p.Personas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}