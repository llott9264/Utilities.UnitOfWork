namespace Utilities.UnitOfWork.Tests.Helpers;

public class Address
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public string Street { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string State { get; set; } = string.Empty;
	public string ZipCode { get; set; } = string.Empty;
	public virtual Customer Customer { get; set; } = new();
}
