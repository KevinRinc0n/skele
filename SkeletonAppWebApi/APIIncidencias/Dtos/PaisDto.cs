namespace APIIncidencias.Dtos;

public class PaisDto
{
    public int Id { get; set; }
    public string NombrePais { get; set; }
    public List<DepartamentoDto> Departamentos { get; set; }   
}