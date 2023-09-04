using Dominio.Entities;
using AutoMapper;
using Dominio.Interfaces;
using APIIncidencias.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.util.zlib;

namespace APIIncidencias.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class GeneroController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public GeneroController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<GeneroDto>>> Get()
    {
        var generos = await unitofwork.Generos.GetAllAsync();
        return mapper.Map<List<GeneroDto>>(generos);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<GeneroDto>> Get(int id)
    {
        var genero = await unitofwork.Generos.GetByIdAsync(id);
        return mapper.Map<GeneroDto>(genero);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Genero>> Post(GenerosDto generoDto)
    {
        var genero = this.mapper.Map<Genero>(generoDto);
        this.unitofwork.Generos.Add(genero);
        await unitofwork.SaveAsync();
        if (genero == null){
            return BadRequest();
        }
        generoDto.Id = genero.Id;
        return CreatedAtAction(nameof(Post), new { id = generoDto.Id }, generoDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<GenerosDto>> Put (int id, [FromBody]GenerosDto generoDto)
    {
        if(generoDto == null)
            return NotFound();

        var genero = this.mapper.Map<Genero>(generoDto);
        unitofwork.Generos.Update(genero);
        await unitofwork.SaveAsync();
        return generoDto;     
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var genero = await unitofwork.Generos.GetByIdAsync(id);

        if (genero == null)
        {
            return Notfound();
        }

        unitofwork.Generos.Remove(genero);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException(); 
    }
}