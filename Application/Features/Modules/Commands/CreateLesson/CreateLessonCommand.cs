using MediatR;
using System;

namespace Application.Features.Modules.Commands.CreateLesson;

// "Quiero crear una lección. Necesito el ID del módulo al que pertenece y su título.
// Te devolveré el Guid de la nueva lección."
public record CreateLessonCommand(Guid ModuleId, string LessonTitle) : IRequest<Guid>;