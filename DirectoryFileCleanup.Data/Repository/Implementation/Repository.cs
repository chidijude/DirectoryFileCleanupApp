using DirectoryFileCleanup.Shared.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DirectoryFileCleanup.Shared.Repository.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;

    public Repository(DbContext dbContext)
    {
        _context = dbContext;
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);

    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }

    public T Get(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}
