using MediatR;
using System;

namespace Application.Features.Instructors.Commands.UpdateInstructor;

// El "mensaje": Contiene el Id para identificar al instructor y el nuevo nombre.
// `IRequest` (sin nada más) significa: "Soy una petición que no devuelve ningún valor."
public record UpdateInstructorCommand(Guid Id, string Name) : IRequest;