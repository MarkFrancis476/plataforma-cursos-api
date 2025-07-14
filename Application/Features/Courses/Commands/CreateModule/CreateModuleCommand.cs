using MediatR;
using System;

namespace Application.Features.Courses.Commands.CreateModule;

// "Quiero crear un módulo. Necesito el ID del curso al que pertenece y el título del módulo.
// Cuando termine, te devolveré el Guid del nuevo módulo."
public record CreateModuleCommand(Guid CourseId, string ModuleTitle) : IRequest<Guid>;