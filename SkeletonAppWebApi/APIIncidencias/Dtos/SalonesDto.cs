namespace APIIncidencias.Dtos;

public class SalonesDto
{
    public int Id { get; set; }
    public string NombreSalon { get; set; }
    public int Capacidad { get; set; }
    public List<MatriculaDto> Matriculas{ get; set; } 
}
