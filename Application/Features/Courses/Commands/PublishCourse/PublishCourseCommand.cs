using MediatR;
using System;

namespace Application.Features.Courses.Commands.PublishCourse;

// "Quiero publicar un curso. Solo necesito saber cuál es su ID. No devuelvo nada."
public record PublishCourseCommand(Guid CourseId) : IRequest;