using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Utilities.UnitOfWork.Contracts;

public interface IDbContextBase : IDisposable
{
	IDatabaseFacadeWrapper Database { get; }
	int SaveChanges();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
	DbSet<TEntity> Set<TEntity>() where TEntity : class;
	EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
