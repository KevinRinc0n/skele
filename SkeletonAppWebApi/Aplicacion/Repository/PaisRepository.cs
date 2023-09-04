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

    public override async Task<(int totalRegistros, IEnumerable<Pais> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Paises as  IQueryable<Pais>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePais.ToLower().Contains(search));
        }
        query = query.OrderBy(p => p.NombrePais);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(u => u.Departamentos)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return(totalRegistros, registros);    
    }

    public override async Task<Pais> GetByIdAsync(int id)  
    {
        return await _context.Paises 
        .Include(p => p.Departamentos)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
} 
