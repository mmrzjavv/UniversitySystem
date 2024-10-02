using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseSelection.infrastructure.Utilities.OperationResult;
using static CourseSelection.Application.Course.CourseDto;

namespace CourseSelection.Application.Course
{
    public interface ICourse
    {
        Task<OperationResult> CreateCourse(CreateCourseDto create);
        Task<OperationResult> GetCourses(int pageSize, int pageNumber);
        Task<OperationResult> UpdateCourse(Guid id, UpdateCourseDto update);
        Task<OperationResult> DeleteCourse(Guid id);
    }
}