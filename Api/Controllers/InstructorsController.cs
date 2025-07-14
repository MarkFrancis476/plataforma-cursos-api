using Application.Features.Instructors.Commands.CreateInstructor;
using Application.Features.Instructors.Commands.DeleteInstructor;
using Application.Features.Instructors.Commands.UpdateInstructor;
using Application.Features.Instructors.Queries.GetAllInstructors;
using Application.Features.Instructors.Queries.GetInstructorById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        return CreatedAtAction(nameof(GetInstructorById), new { id = instructorId }, new { Id = instructorId });
    }

    // GET /api/instructors
    [HttpGet]
    public async Task<IActionResult> GetAllInstructors()
    {
        var instructors = await _mediator.Send(new GetAllInstructorsQuery());
        return Ok(instructors);
    }

    // GET /api/instructors/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInstructorById(Guid id)
    {
        var query = new GetInstructorByIdQuery(id);
        var instructor = await _mediator.Send(query);
        return instructor is not null ? Ok(instructor) : NotFound();
    }

    // PUT /api/instructors/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInstructor(Guid id, [FromBody] UpdateInstructorRequest request)
    {
        var command = new UpdateInstructorCommand(id, request.Name);
        await _mediator.Send(command);
        return NoContent(); // 204 No Content es la respuesta estándar para un PUT exitoso
    }

    // DELETE /api/instructors/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInstructor(Guid id)
    {
        var command = new DeleteInstructorCommand(id);
        await _mediator.Send(command);
        return NoContent(); // 204 No Content es la respuesta estándar para un DELETE exitoso
    }
}

// Pequeña clase DTO para el request de Update
public record UpdateInstructorRequest(string Name);