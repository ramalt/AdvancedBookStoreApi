using System.Linq.Expressions;
using BSApp.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace BSApp.Repository.Contracts;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly ApplicationContext _context;

    protected RepositoryBase(ApplicationContext context)
    {
        _context = context;
    }

    public void Create(T entity) => _context.Set<T>().Add(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);

    public void Update(T entity) => _context.Set<T>().Update(entity);
}
