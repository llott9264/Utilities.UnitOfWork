using Utilities.UnitOfWork.Contracts;

namespace Utilities.UnitOfWork.Infrastructure;

public class UnitOfWorkBase(IDbContextBase context) : IUnitOfWorkBase
{
	public void Dispose()
	{
		context.Dispose();
		GC.SuppressFinalize(this);
	}

	public int Complete()
	{
		return context.SaveChanges();
	}

	public async Task<int> CompleteAsync()
	{
		return await context.SaveChangesAsync();
	}

	public int Complete(int commandTimeoutInSeconds)
	{
		int? originalCommandTimeout = context.Database.GetCommandTimeout();
		context.Database.SetCommandTimeout(commandTimeoutInSeconds);

		int numberOfRecordsChanged = context.SaveChanges();
		context.Database.SetCommandTimeout(originalCommandTimeout);

		return numberOfRecordsChanged;
	}

	public async Task<int> CompleteAsync(int commandTimeoutInSeconds)
	{
		int? originalCommandTimeout = context.Database.GetCommandTimeout();
		context.Database.SetCommandTimeout(commandTimeoutInSeconds);

		int numberOfRecordsChanged = await context.SaveChangesAsync();
		context.Database.SetCommandTimeout(originalCommandTimeout);

		return numberOfRecordsChanged;
	}
}
