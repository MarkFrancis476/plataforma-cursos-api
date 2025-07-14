using MediatR;
using System;

namespace Application.Features.Instructors.Commands.DeleteInstructor;

// El "mensaje": Solo necesita el Id para saber a quién borrar.
public record DeleteInstructorCommand(Guid Id) : IRequest;