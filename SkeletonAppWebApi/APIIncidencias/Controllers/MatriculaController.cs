using Dominio.Entities;
using AutoMapper;
using Dominio.Interfaces;
using APIIncidencias.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APIIncidencias.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class MatriculaController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public MatriculaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MatriculaDto>>> Get()
    {
        var matriculas = await unitofwork.Matriculas.GetAllAsync();
        return mapper.Map<List<MatriculaDto>>(matriculas);
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MatriculaDto>> Get (int id)
    {
        var matricula = await unitofwork.Matriculas.GetByIdAsync(id);
        return mapper.Map<MatriculaDto>(matricula);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Matricula>> Post(MatriculaDto matriculaDto)
    {
        var matricula = this.mapper.Map<Matricula>(matriculaDto);
        this.unitofwork.Matriculas.Add(matricula);
        await unitofwork.SaveAsync();
        if (matricula == null)
        {
            return BadRequest();
        }
        matriculaDto.Id = matricula.Id;
        return CreatedAtAction(nameof(Post), new { id = matriculaDto.Id }, matriculaDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MatriculaDto>> Put (int id, [FromBody]MatriculaDto matriculaDto)
    {
        if(matriculaDto == null)
            return NotFound();

        var matricula = this.mapper.Map<Matricula>(matriculaDto);
        unitofwork.Matriculas.Update(matricula);
        await unitofwork.SaveAsync();
        return matriculaDto;     
    }

    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var matricula = await unitofwork.Matriculas.GetByIdAsync(id);

        if (matricula == null)
        {
            return Notfound();
        }

        unitofwork.Matriculas.Remove(matricula);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException(); 
    }
}