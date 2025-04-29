using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Utilities.UnitOfWork.Contracts;

namespace Utilities.UnitOfWork.Tests.Helpers;

internal class MockContext
{
	public MockContext()
	{
		DbSetAddress = GetAddresses();
		DbSetCustomer = GetCustomers();
		DatabaseFacadeMock = GetMockDatabaseFacade();
		DbContextMock = GetMockContext();
	}

	private Mock<DbSet<Customer>> DbSetCustomer { get; }
	private Mock<DbSet<Address>> DbSetAddress { get; }
	public Mock<IDatabaseFacadeWrapper> DatabaseFacadeMock { get; }
	public Mock<IDbContext> DbContextMock { get; }

	private Mock<IDbContext> GetMockContext()
	{
		{
			Mock<IDbContext> mockContext = new();
			_ = mockContext.Setup(m => m.Customers).Returns(DbSetCustomer.Object);
			_ = mockContext.Setup(m => m.Addresses).Returns(DbSetAddress.Object);
			_ = mockContext.Setup(m => m.Set<Customer>()).Returns(DbSetCustomer.Object);
			_ = mockContext.Setup(m => m.Set<Address>()).Returns(DbSetAddress.Object);
			_ = mockContext.Setup(m => m.SaveChanges()).Returns(2);
			_ = mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(3));
			_ = mockContext.Setup(m => m.Dispose());
			_ = mockContext.Setup(m => m.Database).Returns(DatabaseFacadeMock.Object);
			return mockContext;
		}
	}

	private static Mock<DbSet<Customer>> GetCustomers()
	{
		List<Customer> customers =
		[
			new()
			{
				Id = 6,
				FirstName = "John",
				LastName = "Doe",
				SocialSecurityNumber = "123456789",
				Addresses =
				[
					new Address
					{
						Id = 1,
						CustomerId = 6,
						Street = "123 Main Street",
						City = "Walker",
						State = "LA",
						ZipCode = "70785"
					}
				]
			},
			new()
			{
				Id = 5,
				FirstName = "Joe",
				LastName = "Jones",
				SocialSecurityNumber = "987654321",
				Addresses =
				[
					new Address
					{
						Id = 2,
						CustomerId = 5,
						Street = "456 Sunset Blvd.",
						City = "Baton Rouge",
						State = "LA",
						ZipCode = "70816"
					}
				]
			}
		];

		return customers.AsQueryable().BuildMockDbSet();
	}

	private static Mock<DbSet<Address>> GetAddresses()
	{
		List<Address> addresses =
		[
			new()
			{
				Id = 1,
				CustomerId = 6,
				Street = "123 Main Street",
				City = "Walker",
				State = "LA",
				ZipCode = "70785",
				Customer = new Customer
				{
					Id = 6,
					FirstName = "John",
					LastName = "Doe",
					SocialSecurityNumber = "123456789"
				}
			},

			new()
			{
				Id = 2,
				CustomerId = 5,
				Street = "456 Sunset Blvd.",
				City = "Baton Rouge",
				State = "LA",
				ZipCode = "70816",
				Customer = new Customer
				{
					Id = 5,
					FirstName = "Joe",
					LastName = "Jones",
					SocialSecurityNumber = "987654321"
				}
			}
		];

		return addresses.AsQueryable().BuildMockDbSet();
	}

	private static Mock<IDatabaseFacadeWrapper> GetMockDatabaseFacade()
	{
		Mock<IDatabaseFacadeWrapper> databaseFacadeMock = new();
		_ = databaseFacadeMock.Setup(d => d.GetCommandTimeout()).Returns(10);
		databaseFacadeMock.Setup(d => d.SetCommandTimeout(30)).Verifiable();
		databaseFacadeMock.Setup(d => d.SetCommandTimeout(10)).Verifiable();
		return databaseFacadeMock;
	}
}
