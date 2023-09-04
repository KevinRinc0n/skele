namespace Dominio.Interfaces;

public interface IUnitOfWork 
{
    IPersona Personas{ get; }
    IPais Paises{ get; }
    IDepartamento Departamentos{ get; }
    ICiudad Ciudades{ get; } 
    ISalon Salones{ get; }
    IGenero Generos{ get; }
    IMatricula Matriculas{ get; }
    ITipoPersona TipoPersonas{ get; }
    // ITrainerSalon TrainerSalones{ get; }


    Task<int> SaveAsync();
} 
