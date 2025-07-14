using Domain.Entities;
using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features.Instructors.Commands.CreateInstructor;

public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInstructorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad de dominio
        var instructor = new Instructor(request.Name);

        // 2. Añadirlo a través del repositorio
        await _unitOfWork.InstructorRepository.AddAsync(instructor, cancellationToken);

        // 3. Guardar todos los cambios en la base de datos de una sola vez
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Devolver el ID
        return instructor.Id;
    }
}