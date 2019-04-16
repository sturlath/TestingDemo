using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Entities;

namespace TestingDemo.Repository
{
    /// <summary>
    /// A very generic repository with lots of methods that can be used in each 
    /// special repository for special actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeExpressions)
        {
            // Got this code from here http://appetere.com/post/passing-include-statements-into-a-repository
            if (includeExpressions.Any())
            {
                IQueryable<T> set = includeExpressions
                  .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (dbContext.Set<T>(), (current, expression) => current.Include(expression));

                return await set.SingleOrDefaultAsync(s => s.Id == id);
            }

            return dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetOneByMatchAsync(Expression<Func<T, bool>> match)
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync(match);
        }

        /// <summary>
        /// Use sparingly! You are getting EVERY item!
        /// </summary>
        /// <returns>Every set of T</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetByMatchAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                IQueryable<T> set = includeExpressions
                  .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (dbContext.Set<T>(), (current, expression) => current.Include(expression));

                return await set.Where(match).ToListAsync();
            }

            return await dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            dbContext.Set<T>().Add(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                return null;
            T exist = await dbContext.Set<T>().FindAsync(entity.Id);
            if (exist != null)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.Entry(exist).CurrentValues.SetValues(entity);
                await SaveAsync();
            }
            return exist;
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            T exist = await dbContext.Set<T>().FindAsync(id);
            if (exist != null)
            {
                dbContext.Set<T>().Remove(exist);
                return await SaveAsync();
            }

            //TODO: Find a better example of the delete method to use!
            return 0;
        }

        public async Task<int> CountAsync()
        {
            return await dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

             private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}