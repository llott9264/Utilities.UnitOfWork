namespace Utilities.UnitOfWork.Tests.Helpers;

public class Customer
{
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string SocialSecurityNumber { get; set; } = string.Empty;
	public virtual ICollection<Address> Addresses { get; set; } = [];
	public bool IsRevoked { get; set; } = false;
}
