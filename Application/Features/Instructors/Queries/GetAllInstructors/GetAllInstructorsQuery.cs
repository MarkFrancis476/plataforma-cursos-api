using Domain.Entities;
using MediatR;
using System.Collections.Generic; // Necesario para IReadOnlyList

namespace Application.Features.Instructors.Queries.GetAllInstructors;

// Este es el "mensaje". Fíjate que no tiene parámetros en los paréntesis `()`.
// `IRequest<IReadOnlyList<Instructor>>` significa:
// "Soy una petición que, cuando sea procesada, devolverá una lista de solo lectura de objetos Instructor".
public record GetAllInstructorsQuery() : IRequest<IReadOnlyList<Instructor>>;