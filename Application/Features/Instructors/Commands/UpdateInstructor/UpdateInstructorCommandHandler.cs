using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Instructors.Commands.UpdateInstructor;

// El "manejador": Sabe cómo procesar un UpdateInstructorCommand.
public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInstructorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // El método Handle para un IRequest no devuelve nada, por eso es solo `Task` y no `Task<T>`.
    public async Task Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
    {
        // --- LÓGICA DE LA ACTUALIZACIÓN ---

        // 1. Buscar la entidad que queremos modificar en la base de datos.
        var instructorToUpdate = await _unitOfWork.InstructorRepository.GetByIdAsync(request.Id, cancellationToken);

        // 2. Validar que la entidad exista. Si no, es un error.
        if (instructorToUpdate is null)
        {
            // Aquí deberíamos lanzar una excepción específica (por ejemplo, NotFoundException).
            // Por ahora, una excepción genérica es suficiente.
            throw new Exception($"Instructor con ID '{request.Id}' no fue encontrado.");
        }

        // 3. Llamar al método de la entidad de dominio para realizar el cambio.
        // NUNCA hacemos `instructorToUpdate.Name = request.Name` directamente aquí.
        // Dejamos que la entidad se encargue de su propio estado.
        instructorToUpdate.UpdateName(request.Name);

        // 4. Persistir los cambios en la base de datos.
        // No necesitamos llamar a `_unitOfWork.InstructorRepository.Update()`, porque
        // Entity Framework ya está "rastreando" la entidad `instructorToUpdate` y sabe que ha cambiado.
        // Solo necesitamos guardar.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}