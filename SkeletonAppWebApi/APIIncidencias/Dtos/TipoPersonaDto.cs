namespace APIIncidencias.Dtos;

public class TipoPersonaDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public List<PersonaDto> Personas { get; set; }   
}