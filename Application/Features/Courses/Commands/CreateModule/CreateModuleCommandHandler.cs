using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using System;
using System.Linq; // Necesario para usar el método .Last()
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Courses.Commands.CreateModule;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateModuleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        // 1. Cargar la entidad "raíz" (el curso) usando el nuevo método del repositorio.
        var course = await _unitOfWork.CourseRepository.GetByIdWithModulesAsync(request.CourseId, cancellationToken);

        // 2. Validar que el curso exista.
        if (course is null)
        {
            throw new Exception($"Curso con ID '{request.CourseId}' no fue encontrado.");
        }

        // 3. Crear la nueva entidad de dominio (el módulo).
        var newModule = new Module(request.ModuleTitle, course.Id);

        // 4. ¡LA MAGIA! Delegar la acción a la entidad de dominio.
        // El método `course.AddModule` contiene la regla de negocio que verifica si `IsPublished` es true.
        // Si lo es, lanzará una InvalidOperationException y la ejecución se detendrá aquí.
        course.AddModule(newModule);

        // 5. Persistir los cambios. El UnitOfWork guardará tanto la modificación en la
        // lista de módulos del curso como la creación del nuevo módulo.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 6. Devolver el ID de la nueva entidad creada.
        // Asumimos que el nuevo módulo es el último en la lista.
        return newModule.Id;
    }
}