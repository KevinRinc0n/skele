using APIIncidencias.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIIncidencias.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
 
public class PersonaController : BaseApiController 
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper _mapper;

    public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var Personas = await unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<PersonaDto>>(Personas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Get(int id)
    {
        var Persona = await unitOfWork.Personas.GetByIdAsync(id);
        return _mapper.Map<PersonaDto>(Persona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Persona>> Post(PersonasDto personaDto)
    {

        var persona = _mapper.Map<Persona>(personaDto);
        this.unitOfWork.Personas.Add(persona);
        await unitOfWork.SaveAsync();

        if(persona == null)
        {
            return BadRequest();
        }
        personaDto.Id = persona.Id;
        return CreatedAtAction(nameof(Post), new { id = personaDto.Id }, personaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<PersonasDto>> Put(int id, [FromBody]PersonasDto personaDto)
    {
        if(personaDto == null)
        {
            return NotFound();
        }
        var personas = _mapper.Map<Persona>(personaDto);
        unitOfWork.Personas.Update(personas);
        await unitOfWork.SaveAsync();
        return personaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var persona = await unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null)
            return NotFound();

        unitOfWork.Personas.Remove(persona);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}