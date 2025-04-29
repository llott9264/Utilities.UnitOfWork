using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Utilities.UnitOfWork.Contracts;

namespace Utilities.UnitOfWork.Infrastructure;

public class DatabaseFacadeWrapper(DatabaseFacade databaseFacade) : IDatabaseFacadeWrapper
{
	public int? GetCommandTimeout()
	{
		return databaseFacade.GetCommandTimeout();
	}

	public void SetCommandTimeout(int? timeout)
	{
		databaseFacade.SetCommandTimeout(timeout);
	}

	public bool EnsureCreated()
	{
		return databaseFacade.EnsureCreated();
	}

	public void OpenConnection()
	{
		databaseFacade.OpenConnection();
	}
}
