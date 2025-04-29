using Microsoft.EntityFrameworkCore;
using Utilities.UnitOfWork.Contracts;

namespace Utilities.UnitOfWork.Tests.Helpers;

public interface IDbContext : IDbContextBase
{
	DbSet<Customer> Customers { get; set; }
	DbSet<Address> Addresses { get; set; }
}
