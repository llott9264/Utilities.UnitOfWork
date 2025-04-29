namespace Utilities.UnitOfWork.Contracts;

public interface IUnitOfWorkBase : IDisposable
{
	int Complete();
	Task<int> CompleteAsync();
	int Complete(int commandTimeoutInSeconds);
	Task<int> CompleteAsync(int commandTimeoutInSeconds);
}
