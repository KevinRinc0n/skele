using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Persistencia.Repository;

public class DepartamentoRepository : GenericRepo<Departamento>, IDepartamento
{
    private readonly Incidenciascontext _context;
    public DepartamentoRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
            .Include(p => p.Ciudades)
            .ToListAsync();
    }

    public override async Task<Departamento> GetByIdAsync(int id)
    {
        return await _context.Departamentos
        .Include(p => p.Ciudades)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}