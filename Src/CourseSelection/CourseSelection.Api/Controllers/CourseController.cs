using Asp.Versioning;
using CourseSelection.Application.Course;
using CourseSelection.infrastructure.Utilities.OperationResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static CourseSelection.Application.Course.CourseDto;

namespace CourseSelection.Api.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{v:apiVersion}/Courses")]
    public class CourseController(ICourse course) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OperationResult>> CreateCourse(CreateCourseDto create)
        {
            var data = await course.CreateCourse(create);
            if (!data.Success)
            {
                StatusCode((int)data.Status, data);
            }

            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResult>> GetCourse([FromQuery] int pageNumber, int pageSize)
        {
            var data = await course.GetCourses(pageSize, pageNumber);
            if (!data.Success)
            {
                StatusCode((int)data.Status, data);
            }

            return Ok(data);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OperationResult>> UpdateCourse(Guid id, UpdateCourseDto update)
        {
            var data = await course.UpdateCourse(id, update);
            if (!data.Success)
            {
                StatusCode((int)data.Status, data);
            }

            return Ok(data);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<OperationResult>> DeleteCourse(Guid id)
        {
            var data = await course.DeleteCourse(id);
            if (!data.Success)
            {
                StatusCode((int)data.Status, data);
            }

            return Ok(data);
        }
    }
}