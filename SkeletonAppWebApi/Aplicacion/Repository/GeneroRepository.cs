using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Persistencia.Repository;

public class GeneroRepository : GenericRepo<Genero>, IGenero
{
    private readonly Incidenciascontext _context;
    public GeneroRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Genero>> GetAllAsync()
    {
        return await _context.Generos
            .Include(p => p.Personas)
            .ToListAsync();
    }

    public override async Task<Genero> GetByIdAsync(int id)
    {
        return await _context.Generos
        .Include(p => p.Personas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}