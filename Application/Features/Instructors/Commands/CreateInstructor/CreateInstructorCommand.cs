using MediatR;

namespace Application.Features.Instructors.Commands.CreateInstructor;

// Define los datos de entrada y el tipo de dato de salida (el Guid del nuevo instructor)
public record CreateInstructorCommand(string Name) : IRequest<Guid>;