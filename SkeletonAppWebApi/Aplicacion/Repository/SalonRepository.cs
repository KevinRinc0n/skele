using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Aplicacion.Repository;

public class SalonRepository : GenericRepo<Salon>, ISalon
{
    protected readonly Incidenciascontext _context;

    public SalonRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Salon>> GetAllAsync()
    {
        return await _context.Salones
        .Include(p => p.Matriculas)
        .ToListAsync();
    }

    public override async Task<Salon> GetByIdAsync(int id) 
    {
        return await _context.Salones
        .Include(p => p.Matriculas)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}