using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Features.Instructors.Commands.CreateInstructor;
using Application.Features.Instructors.Queries.GetInstructorById; // Necesario para el CreatedAtAction

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorsController : ControllerBase
{
    private readonly ISender _mediator;

    public InstructorsController(ISender mediator)
    {
        _mediator = mediator;
    }

    // POST /api/instructors
    [HttpPost]
    public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorCommand command)
    {
        var instructorId = await _mediator.Send(command);
        
        // Para que CreatedAtAction funcione, necesitamos un endpoint que obtenga un instructor por ID.
        // Lo implementaremos correctamente más tarde.
        return CreatedAtAction(nameof(GetInstructorById), new { id = instructorId }, new { Id = instructorId });
    }

    // GET /api/instructors/{id}
    // Este método es necesario para que la línea de arriba funcione.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInstructorById(Guid id)
    {
        var query = new GetInstructorByIdQuery(id);
        var instructor = await _mediator.Send(query);
        
        return instructor is not null ? Ok(instructor) : NotFound();
    }
}