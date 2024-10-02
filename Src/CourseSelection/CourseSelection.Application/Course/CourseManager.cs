using System.Net;
using AutoMapper;
using Azure.Core;
using CourseSelection.Domain;
using CourseSelection.infrastructure.Utilities.OperationResult;

namespace CourseSelection.Application.Course
{
    public class CourseManager(IUnitOfWork unitOfWork, IMapper mapper) : ICourse
    {
        public async Task<OperationResult> CreateCourse(CourseDto.CreateCourseDto create)
        {
            try
            {
                var createCourse = await unitOfWork.CreateAsync<Domain.Course.Courses>(new Domain.Course.Courses
                {
                    Name = create.Name,
                    UnitsCount = create.UnitsCount,
                });

                return !createCourse
                    ? OperationResult.CreateFailure("خطا در ذخیره اطلاعات")
                    : OperationResult.CreateSuccess("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure("خطا در طی عملیات", ex.Message,
                    System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<OperationResult> GetCourses(int pageSize, int pageNumber)
        {
            try
            {
                var data = await unitOfWork.GetPagedListAsync<Domain.Course.Courses>(pageNumber, pageSize);
                return data.Count == 0
                    ? OperationResult.CreateFailure("اطلاعات یافت نشد", null, System.Net.HttpStatusCode.NotFound)
                    : OperationResult.CreateSuccess("اطلاعات با موفقیت یافت شد", null,
                        mapper.Map<List<CourseDto.GetCoursesDto>>(data));
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure("خطا در طی عملیات", ex.Message,
                    System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult> UpdateCourse(Guid id, CourseDto.UpdateCourseDto update)
        {
            try
            {
                var isExist = await unitOfWork.ExistsAsync<Domain.Course.Courses>(x => x.Id == id);
                if (!isExist)
                {
                    return OperationResult.CreateFailure("اطلاعات جهت بروزرسانی وجود ندارد", null,
                        System.Net.HttpStatusCode.NotFound);
                }

                var updateCourse = await unitOfWork.UpdateAsync<Domain.Course.Courses>(new Domain.Course.Courses
                {
                    Id = id,
                    Name = update.Name,
                    UnitsCount = update.UnitsCount,
                    ModifyDate = DateTime.Now
                });
                return !updateCourse
                    ? OperationResult.CreateFailure("خطا در بروزرسانی ")
                    : OperationResult.CreateSuccess("اطلاعات با موفقیت بروزرسانی شد");
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure("خطا در طی عملیات", ex.Message,
                    System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult> DeleteCourse(Guid id)
        {
            try
            {
                var isExist = await unitOfWork.ExistsAsync<Domain.Course.Courses>(x => x.Id == id);
                if (!isExist)
                {
                    return OperationResult.CreateFailure
                        ("اطلاعات جهت حذف وجود ندارد", null, HttpStatusCode.NotFound);
                }

                var deleteCourse = await unitOfWork.DeleteAsync<Domain.Course.Courses>(id);
                return !deleteCourse
                    ? OperationResult.CreateFailure("خطا در حذف اطلاعات")
                    : OperationResult.CreateSuccess("اطلاعات با موفقیت حذف شد");
            }
            catch (Exception ex)
            {
                return OperationResult.CreateFailure("خطا در طی عملیات", ex.Message,
                    System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

