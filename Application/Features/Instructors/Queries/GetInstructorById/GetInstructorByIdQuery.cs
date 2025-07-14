using MediatR;
using Domain.Entities; // Por ahora devolveremos la entidad directamente

namespace Application.Features.Instructors.Queries.GetInstructorById;

// "Quiero los datos de un instructor, necesito su Id, y te devolveré un objeto Instructor"
public record GetInstructorByIdQuery(Guid Id) : IRequest<Instructor?>;