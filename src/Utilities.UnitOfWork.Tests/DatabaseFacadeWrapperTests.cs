using Microsoft.EntityFrameworkCore;
using Utilities.UnitOfWork.Tests.Helpers;

namespace Utilities.UnitOfWork.Tests;

public class DatabaseFacadeWrapperTests
{
	private ApplicationDbContext CreateInMemoryContext()
	{
		DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder =
			new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseSqlite("DataSource=:memory:");

		_ = optionsBuilder.UseSqlite("DataSource=:memory:", sqliteOptions => { _ = sqliteOptions.CommandTimeout(30); });

		DbContextOptions<ApplicationDbContext> options = optionsBuilder.Options;
		ApplicationDbContext context = new(options);
		context.Database.OpenConnection();
		context.Database.EnsureCreated();
		return context;
	}

	[Fact]
	public void GetCommandTimeout_ReturnsDefaultTimeout()
	{
		// Arrange
		const int expectedTimeout = 30;

		using (ApplicationDbContext context = CreateInMemoryContext())
		{
			// Act
			int? commandTimeout = context.Database.GetCommandTimeout();


			// Assert
			Assert.Equal(expectedTimeout, commandTimeout);
		}
	}

	[Fact]
	public void SetCommandTimeout_ReturnsNewTimeout()
	{
		// Arrange
		const int expectedTimeout = 120;
		using (ApplicationDbContext context = CreateInMemoryContext())
		{
			// Act
			context.Database.SetCommandTimeout(expectedTimeout);
			int? newCommandTimeout = context.Database.GetCommandTimeout();

			// Assert
			Assert.Equal(expectedTimeout, newCommandTimeout);
		}
	}
}
