using Dominio.Entities;
using AutoMapper;
using Dominio.Interfaces;
using APIIncidencias.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APIIncidencias.Controllers;

public class DepartamentoController : BaseApiController
{
    private IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    } 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentosDto>>> Get()
    {
        var departamentos = await unitofwork.Departamentos.GetAllAsync();
        return mapper.Map<List<DepartamentosDto>>(departamentos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartamentoDto>> Get(int id)
    {
        var departamento = await unitofwork.Departamentos.GetByIdAsync(id);
        return mapper.Map<DepartamentoDto>(departamento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentosDto departamentoDto)
    {
        var departamento = this.mapper.Map<Departamento>(departamentoDto);
        this.unitofwork.Departamentos.Add(departamento);
        await unitofwork.SaveAsync();
        if (departamento == null){
            return BadRequest();
        }
        departamentoDto.Id = departamento.Id;
        return CreatedAtAction(nameof(Post), new { id = departamentoDto.Id }, departamentoDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DepartamentosDto>> Put (int id, [FromBody]DepartamentosDto departamentoDto)
    {
        if(departamentoDto == null)
            return NotFound();

        var departamento = this.mapper.Map<Departamento>(departamentoDto);
        unitofwork.Departamentos.Update(departamento);
        await unitofwork.SaveAsync();
        return departamentoDto;     
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var departamento = await unitofwork.Departamentos.GetByIdAsync(id);

        if (departamento == null)
        {
            return Notfound();
        }

        unitofwork.Departamentos.Remove(departamento);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}