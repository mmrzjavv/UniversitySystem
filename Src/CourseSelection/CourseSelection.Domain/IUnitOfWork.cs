using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelection.Domain
{
    public interface IUnitOfWork
    {
        Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, bool disableTracking = true, bool isFirst = true) where T : BaseEntity.BaseEntity;
        Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, string? includeString = null, bool disableTracking = true, bool isFirst = true) where T : BaseEntity.BaseEntity;    
        Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true, bool isFirst = true) where T : BaseEntity.BaseEntity;

        Task<List<T>> GetPagedListAsync<T>(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, List<Expression<Func<T, object>>>? includes = null, string? orderBy = null, bool orderByDescending = true, bool disableTracking = true) where T : BaseEntity.BaseEntity;
        Task<List<T>> GetAllAsync<T>(List<Expression<Func<T, object>>>? includes = null, string? orderBy = null, bool orderByDescending = true, bool disableTracking = true) where T : BaseEntity.BaseEntity;

        Task<int> CountAsync<T>(Expression<Func<T, bool>>? predicate = null) where T : BaseEntity.BaseEntity;
        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity.BaseEntity;

        Task<bool> CreateAsync<T>(T model) where T : BaseEntity.BaseEntity;
        Task<bool> CreateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity.BaseEntity;
        Task<bool> BulkInsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity.BaseEntity;

        Task<bool> UpdateAsync<T>(T model) where T : BaseEntity.BaseEntity;
        Task<bool> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity.BaseEntity;

        Task<bool> DeleteAsync<T>(Guid id) where T : BaseEntity.BaseEntity;
        Task<bool> DeleteRangeAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity.BaseEntity;

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
