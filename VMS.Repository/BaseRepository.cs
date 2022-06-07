using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VMS.Entities;
using X.PagedList;

namespace VMS.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, IObjectWithState
    {
        protected readonly VMSDatabaseContext db;

        public BaseRepository(VMSDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<int> Save(T model)
        {
            try
            {
                return await db.ApplyChanges(model);
            }
            catch (DbUpdateConcurrencyException dce)
            {
                throw new DBConcurrencyException(dce.Message, dce.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = "")
        {
            IQueryable<T> query = db.Set<T>();

            foreach (var includeProperty in includeProperties.Split(new[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(filter) ?? throw new Exception("Unable to query the data");
        }

        public virtual IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public virtual async Task<IEnumerable<T>> GetPageableList(int? pageNumber, int pageSize, string includeProperties = "")
        {
            IQueryable<T> query = db.Set<T>();

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            var page = pageNumber ?? 1;

            return await query.AsNoTracking().ToPagedListAsync(page, pageSize);
        }

        public virtual async Task<IEnumerable<T>> GetList(string includeProperties = "")
        {
            IQueryable<T> query = db.Set<T>();

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().AsQueryable().Where(predicate).ToListAsync();
        }

        public virtual async Task<int> Delete(T model)
        {
           return await db.ApplyChanges(model);
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}