using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia.Data;
using Persistencia.Repository;

namespace Persistencia.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Incidenciascontext context;
    private PaisRepository _paises;
    private DepartamentoRepository _departamentos;
    private CiudadRepository _ciudades;
    private SalonRepository _salones; 
    private GeneroRepository _generos;
    private MatriculaRepository _matriculas;
    private TipoPersonaRepository _tipoPersonas;
    // private TrainerSalonRepository _trainerSalones;


    public UnitOfWork(Incidenciascontext _context) 
    {
        context = _context;
    }

    public IPais Paises
    {
        get{
            if (_paises == null)
            {
                _paises = new PaisRepository(context); 
            }
            return _paises;
        }

    }

    public IDepartamento Departamentos
    {
        get{
            if (_departamentos == null)
            {
                _departamentos = new DepartamentoRepository(context);
            }
            return _departamentos;
        }

    }

    public ICiudad Ciudades
    {
        get{
            if (_ciudades == null)
            {
                _ciudades = new CiudadRepository(context);
            }
            return _ciudades;
        }

    }

    public ISalon Salones
    {
        get{
            if (_salones == null)
            {
                _salones = new SalonRepository(context);
            }
            return _salones;
        }

    }
    
    public IGenero Generos
    {
        get{
            if (_generos == null)
            {
                _generos = new GeneroRepository(context);
            }
            return _generos;
        }

    }

    public IMatricula Matriculas
    {
        get{
            if (_matriculas == null)
            {
                _matriculas = new MatriculaRepository(context);
            }
            return _matriculas;
        }

    }

    public ITipoPersona TipoPersonas
    {
        get{
            if (_tipoPersonas == null)
            {
                _tipoPersonas = new TipoPersonaRepository(context);
            }
            return _tipoPersonas;
        }

    }

    // public ITrainerSalon TrainerSalones
    // {
    //     get{
    //         if (_trainerSalones == null)
    //         {
    //             _trainerSalones = new TrainerSalonRepository(context);
    //         }
    //         return _trainerSalones;
    //     }

    // }

    public IPersona Personas => throw new NotImplementedException();

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}

