using Microsoft.EntityFrameworkCore;
using Nano_Health.Data;
using System.Linq.Expressions;

namespace Nano_Health.Loc
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected NanoHealthDbContext context;
        public Repository(NanoHealthDbContext context)
        {
            this.context = context;
        }
        public IQueryable<T> Table => context.Set<T>();
        public T GetById(long id)
        {
            return context.Set<T>().Find(id);
        }

        public T SelectOne(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public IEnumerable<T> GetAll(params string[] agers)
        {
            IQueryable<T> query = context.Set<T>();

            if (agers.Length > 0)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] agers)
        {
            IQueryable<T> query = context.Set<T>();

            if (agers.Length > 0)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }
            }

            return await query.ToListAsync();
        }
        //=========================================================================//
        public async Task AddAsync(T myItem)
        {
            await context.Set<T>().AddAsync(myItem);
        }

        public void Update(T myItem)
        {
            context.Set<T>().Update(myItem);
        }

        public void Delete(T myItem)
        {
            context.Set<T>().Remove(myItem);
        }

        public async Task AddListAsync(IEnumerable<T> myList)
        {
            await context.Set<T>().AddRangeAsync(myList);
        }

        public void UpdateList(IEnumerable<T> myList)
        {
            context.Set<T>().UpdateRange(myList);
        }

        public void DeleteList(IEnumerable<T> myList)
        {
            context.Set<T>().RemoveRange(myList);
        }
    }
}
