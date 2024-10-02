using CourseSelection.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CourseSelection.infrastructure.Persistance;
using CourseSelection.Domain.BaseEntity;

namespace CourseSelection.infrastructure.Service
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly CourseSelectionContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWorkRepository(CourseSelectionContext context) => _context = context;
        private DbSet<T> GetDbSet<T>() where T : class
        {
            return _context.Set<T>();
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, bool disableTracking = true, bool isFirst = true) where T : BaseEntity
        {
            IQueryable<T> query = GetDbSet<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            query = query.OrderByDescending(x => x.CreateDate);

            return isFirst ? await query.FirstOrDefaultAsync(predicate) : await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, string? includeString = null, bool disableTracking = true, bool isFirst = true) where T : BaseEntity
        {
            IQueryable<T> query = GetDbSet<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrEmpty(includeString))
            {
                query = query.Include(includeString);
            }
            query = query.OrderByDescending(x => x.CreateDate);

            return isFirst ? await query.FirstOrDefaultAsync(predicate) : await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true, bool isFirst = true) where T : BaseEntity
        {
            IQueryable<T> query = GetDbSet<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            query = query.OrderByDescending(x => x.CreateDate);

            return isFirst ? await query.FirstOrDefaultAsync(predicate) : await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetPagedListAsync<T>(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, List<Expression<Func<T, object>>>? includes = null, string? orderBy = null, bool orderByDescending = true, bool disableTracking = true) where T : BaseEntity
        {
            IQueryable<T> query = GetDbSet<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = orderByDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }
            if (orderByDescending)
            {
                query = query.OrderByDescending(x => x.CreateDate);
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<T>(List<Expression<Func<T, object>>>? includes = null, string? orderBy = null, bool orderByDescending = true, bool disableTracking = true) where T : BaseEntity
        {
            IQueryable<T> query = GetDbSet<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = orderByDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }
            if (orderByDescending)
            {
                query = query.OrderByDescending(x => x.CreateDate);
            }

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync<T>(Expression<Func<T, bool>>? predicate = null) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            return predicate != null ? await dbSet.CountAsync(predicate) : await dbSet.CountAsync();
        }

        public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            return await dbSet.AnyAsync(predicate);
        }

        public async Task<bool> CreateAsync<T>(T model) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            await dbSet.AddAsync(model);
            var success = await _context.SaveChangesAsync() > 0;
            //_logger.LogInformation($"A record in the {typeof(T).Name} table was created by the {model.Id} id on the {DateTime.Now} date");
            return success;
        }

        public async Task<bool> CreateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            await dbSet.AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> BulkInsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            await dbSet.AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync<T>(T model) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            var existingEntity = await dbSet.FindAsync(model.GetType().GetProperty("Id")?.GetValue(model));

            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(model);
            var success = await _context.SaveChangesAsync() > 0;
            //_logger.LogInformation($"A record in the {typeof(T).Name} table was Updated by the {model.Id} id on the {DateTime.Now} date");
            return success;
        }

        public async Task<bool> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            dbSet.UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync<T>(Guid id) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            var success = await _context.SaveChangesAsync() > 0;
            //_logger.LogInformation($"A record in the {typeof(T).Name} table was Deleted by the {id} id on the {DateTime.Now} date");
            return success;
        }

        public async Task<bool> DeleteRangeAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            var dbSet = GetDbSet<T>();
            var entities = await dbSet.Where(predicate).ToListAsync();

            if (!entities.Any())
            {
                return false;
            }

            dbSet.RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
