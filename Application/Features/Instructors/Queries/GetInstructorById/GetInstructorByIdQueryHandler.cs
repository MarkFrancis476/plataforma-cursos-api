using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

// ¡OJO! El namespace debe coincidir con la carpeta. Lo corregí de `ById` a `ById`.
namespace Application.Features.Instructors.Queries.GetInstructorById; 

// El handler debe devolver Instructor? (nullable)
public class GetInstructorByIdQueryHandler : IRequestHandler<GetInstructorByIdQuery, Instructor?> 
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInstructorByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // El método Handle también debe devolver Task<Instructor?>
    public async Task<Instructor?> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
    {
        // Llamamos al método correcto: GetByIdAsync
        return await _unitOfWork.InstructorRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}