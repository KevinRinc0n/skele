using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Entities;
using Aplicacion.Repository;

namespace Persistencia.Repository;

public class MatriculaRepository : GenericRepo<Matricula>, IMatricula
{
    private readonly Incidenciascontext _context;
    public MatriculaRepository(Incidenciascontext context) : base(context)
    {
        _context = context;
    }
}