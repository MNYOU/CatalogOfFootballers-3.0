using Dal.Entities;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dal.EFCore.Repositories;

public abstract class EntityRepository<T, TId> : IEntityRepository<T, TId>
    where T : Entity<TId>
    where TId : IEquatable<TId>
{
    protected EntityRepository(DataContext context, DbSet<T> items)
    {
        Context = context;
        Items = items;
    }
    
    protected DataContext Context { get; }

    public DbSet<T> Items { get; }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int SaveChanges()
    {
        return Context.SaveChanges();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken); 
    }

    public virtual async Task<T?> FindAsync(TId id)
    {
        return await Items.FindAsync(id);
    }
    
    public IQueryable<T> GetAll()
    {
        return Items;
    }

    public IQueryable<T> FindBy(Predicate<T> predicate)
    {
        return Items.Where(x => predicate(x));
    }

    public void Add(T entity)
    {
        Items.Add(entity);
    }

    public void Update(T entity)
    {
        Items.Update(entity);
    }

    public void Delete(T entity)
    {
        Items.Remove(entity);
    }
}