using Utilities.UnitOfWork.Contracts;
using Utilities.UnitOfWork.Infrastructure;

namespace Utilities.UnitOfWork.Tests.Helpers;

public class AddressRepository(IDbContextBase context) : RepositoryBase<Address>(context), IAddressRepository
{
}
