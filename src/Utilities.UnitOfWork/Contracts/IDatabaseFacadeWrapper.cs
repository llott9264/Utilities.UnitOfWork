namespace Utilities.UnitOfWork.Contracts;

public interface IDatabaseFacadeWrapper
{
	int? GetCommandTimeout();
	void SetCommandTimeout(int? timeout);
	bool EnsureCreated();
	void OpenConnection();
}
