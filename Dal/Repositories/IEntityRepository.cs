using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public interface IEntityRepository<T, TId> : IDisposable
    where T : Entity<TId>
    where TId : IEquatable<TId>
{
    DbSet<T> Items { get; }
    
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<T?> FindAsync(TId id);

    IQueryable<T> GetAll();

    IQueryable<T> FindBy(Predicate<T> predicate);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);
}