using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Instructors.Queries.GetAllInstructors;

public class GetAllInstructorsQueryHandler : IRequestHandler<GetAllInstructorsQuery, IReadOnlyList<Instructor>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllInstructorsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Instructor>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
    {
        // Aquí usamos el método GetAllAsync que definimos en la interfaz
        var instructors = await _unitOfWork.InstructorRepository.GetAllAsync(cancellationToken);
        
        return instructors;
    }
}