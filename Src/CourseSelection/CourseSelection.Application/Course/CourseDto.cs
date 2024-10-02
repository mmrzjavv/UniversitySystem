using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelection.Application.Course
{
    public class CourseDto
    {
        public record CreateCourseDto
            (string Name, int UnitsCount);
        
        public record GetCoursesDto
            (Guid Id , string Name, int UnitsCount , DateTime CreateDate , DateTime ModifyDate);
        
        public record UpdateCourseDto 
            (string Name, int UnitsCount);
    }
}

