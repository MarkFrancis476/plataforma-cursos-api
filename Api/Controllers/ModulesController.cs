using Application.Features.Modules.Commands.CreateLesson;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/modules")]
public class ModulesController : ControllerBase
{
    private readonly ISender _mediator;

    public ModulesController(ISender mediator)
    {
        _mediator = mediator;
    }

    // POST /api/modules/{moduleId}/lessons
    [HttpPost("{moduleId}/lessons")]
    public async Task<IActionResult> CreateLesson(Guid moduleId, [FromBody] CreateLessonRequest request)
    {
        var command = new CreateLessonCommand(moduleId, request.LessonTitle);
        var lessonId = await _mediator.Send(command);
        return Ok(new { LessonId = lessonId });
    }
}

public record CreateLessonRequest(string LessonTitle);