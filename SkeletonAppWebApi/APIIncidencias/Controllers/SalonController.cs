using Dominio.Entities;
using AutoMapper;
using Dominio.Interfaces;
using APIIncidencias.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APIIncidencias.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class SalonController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public SalonController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<SalonDto>>> Get()
    {
        var salones = await unitofwork.Salones.GetAllAsync();
        return mapper.Map<List<SalonDto>>(salones);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalonDto>> Get(int id)
    {
        var salon = await unitofwork.Salones.GetByIdAsync(id);
        return mapper.Map<SalonDto>(salon);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Salon>> Post(SalonesDto salonDto)
    {
        var salon = this.mapper.Map<Salon>(salonDto);
        this.unitofwork.Salones.Add(salon);
        await unitofwork.SaveAsync();
        if (salon == null){
            return BadRequest();
        }
        salonDto.Id = salon.Id;
        return CreatedAtAction(nameof(Post), new { id = salonDto.Id }, salonDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<SalonesDto>> Put (int id, [FromBody]SalonesDto salonDto)
    {
        if(salonDto == null)
            return NotFound();

        var salon = this.mapper.Map<Salon>(salonDto);
        unitofwork.Salones.Update(salon);
        await unitofwork.SaveAsync();
        return salonDto;     
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> Delete (int id)
    {
        var salon = await unitofwork.Salones.GetByIdAsync(id);

        if (salon == null)
        {
            return Notfound();
        }

        unitofwork.Salones.Remove(salon);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    private ActionResult Notfound()
    {
        throw new NotImplementedException();
    }
}