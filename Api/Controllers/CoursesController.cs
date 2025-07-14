using Application.Features.Courses.Commands.CreateModule;
using Application.Features.Courses.Commands.PublishCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ISender _mediator;

    public CoursesController(ISender mediator)
    {
        _mediator = mediator;
    }

    // POST /api/courses/{courseId}/modules
    [HttpPost("{courseId}/modules")]
    public async Task<IActionResult> CreateModule(Guid courseId, [FromBody] CreateModuleRequest request)
    {
        var command = new CreateModuleCommand(courseId, request.ModuleTitle);
        var moduleId = await _mediator.Send(command);
        return Ok(new { ModuleId = moduleId }); // Devolvemos un 200 OK con el ID del nuevo módulo
    }

    // POST /api/courses/{courseId}/publish
    [HttpPost("{courseId}/publish")]
    public async Task<IActionResult> PublishCourse(Guid courseId)
    {
        var command = new PublishCourseCommand(courseId);
        await _mediator.Send(command);
        return NoContent();
    }
}

public record CreateModuleRequest(string ModuleTitle);