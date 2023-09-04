using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Persistencia.Repository;

public class PaisRepository : GenericRepo<Pais>, IPais
{
    private readonly Incidenciascontext _context;
    public PaisRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pais>> GetAllAsync()
    {
        return await _context.Paises
            .Include(p => p.Departamentos)
            .ToListAsync();
    }

    public override async Task<Pais> GetByIdAsync(int id)  
    {
        return await _context.Paises 
        .Include(p => p.Departamentos)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
} 
