using AutoMapper;

namespace CourseSelection.Application.Course;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<Domain.Course.Courses, CourseDto.GetCoursesDto>().ReverseMap();
    }
}