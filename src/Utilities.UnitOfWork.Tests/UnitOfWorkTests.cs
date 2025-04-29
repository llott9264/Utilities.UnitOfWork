using Moq;
using Utilities.UnitOfWork.Contracts;
using Utilities.UnitOfWork.Infrastructure;
using Utilities.UnitOfWork.Tests.Helpers;

namespace Utilities.UnitOfWork.Tests;

public class UnitOfWorkTests
{
	[Fact]
	public void Complete_ReturnsTwo()
	{
		//Arrange
		Mock<IDbContext> mockContext = new MockContext().DbContextMock;
		IUnitOfWorkBase unitOfWork = new UnitOfWorkBase(mockContext.Object);

		//Act
		int numberOfRecordsChanged = unitOfWork.Complete();

		//Assert
		Assert.True(numberOfRecordsChanged == 2);
	}

	[Fact]
	public async Task CompleteAsync_ReturnsThree()
	{
		//Arrange
		Mock<IDbContext> mockContext = new MockContext().DbContextMock;
		IUnitOfWorkBase unitOfWork = new UnitOfWorkBase(mockContext.Object);

		//Act
		int numberOfRecordsChanged = await unitOfWork.CompleteAsync();

		//Assert
		Assert.True(numberOfRecordsChanged == 3);
	}

	[Fact]
	public void Complete_WithCommandTimeOut_ReturnsTwo()
	{
		//Arrange
		MockContext mockContext = new();
		Mock<IDbContext> dbContextMock = mockContext.DbContextMock;
		Mock<IDatabaseFacadeWrapper> databaseFacadeMock = mockContext.DatabaseFacadeMock;
		IUnitOfWorkBase unitOfWork = new UnitOfWorkBase(dbContextMock.Object);
		const int commandTimeoutInSeconds = 30;
		int? originalTimeout = 10;
		const int expectedRecordsChanged = 2;

		//Act
		int numberOfRecordsChanged = unitOfWork.Complete(commandTimeoutInSeconds);

		//Assert
		databaseFacadeMock.Verify(d => d.GetCommandTimeout(), Times.Once());
		databaseFacadeMock.Verify(d => d.SetCommandTimeout(commandTimeoutInSeconds), Times.Once());
		databaseFacadeMock.Verify(d => d.SetCommandTimeout(originalTimeout), Times.Once());
		dbContextMock.Verify(c => c.SaveChanges(), Times.Once());
		Assert.Equal(expectedRecordsChanged, numberOfRecordsChanged);
	}

	[Fact]
	public async Task CompleteAsync_WithCommandTimeOut_ReturnsTwo()
	{
		//Arrange
		MockContext mockContext = new();
		Mock<IDbContext> dbContextMock = mockContext.DbContextMock;
		Mock<IDatabaseFacadeWrapper> databaseFacadeMock = mockContext.DatabaseFacadeMock;
		IUnitOfWorkBase unitOfWork = new UnitOfWorkBase(dbContextMock.Object);
		const int commandTimeoutInSeconds = 30;
		int? originalTimeout = 10;
		const int expectedRecordsChanged = 3;

		//Act
		int numberOfRecordsChanged = await unitOfWork.CompleteAsync(commandTimeoutInSeconds);

		//Assert
		databaseFacadeMock.Verify(d => d.GetCommandTimeout(), Times.Once());
		databaseFacadeMock.Verify(d => d.SetCommandTimeout(commandTimeoutInSeconds), Times.Once());
		databaseFacadeMock.Verify(d => d.SetCommandTimeout(originalTimeout), Times.Once());
		dbContextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
		Assert.Equal(expectedRecordsChanged, numberOfRecordsChanged);
	}

	[Fact]
	public void Dispose()
	{
		//Arrange
		Mock<IDbContext> mockContext = new MockContext().DbContextMock;
		IUnitOfWorkBase unitOfWork = new UnitOfWorkBase(mockContext.Object);

		//Act
		unitOfWork.Dispose();

		//Assert
		mockContext.Verify(m => m.Dispose(), Times.Once());
	}
}
