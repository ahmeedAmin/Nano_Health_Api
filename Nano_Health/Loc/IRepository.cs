using System.Linq.Expressions;

namespace Nano_Health.Loc
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        T GetById(long id);

        T SelectOne(Expression<Func<T, bool>> match);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(params string[] agers);

        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(params string[] agers);

        Task AddAsync(T myItem);

        void Update(T myItem);

        void Delete(T myItem);

        Task AddListAsync(IEnumerable<T> myList);

        void UpdateList(IEnumerable<T> myList);

        void DeleteList(IEnumerable<T> myList);
    }
}
