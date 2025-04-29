using Microsoft.EntityFrameworkCore;
using Utilities.UnitOfWork.Contracts;
using Utilities.UnitOfWork.Infrastructure;

namespace Utilities.UnitOfWork.Tests.Helpers;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	: DbContext(options), IDbContext
{
	public new IDatabaseFacadeWrapper Database => new DatabaseFacadeWrapper(base.Database);
	public virtual DbSet<Address> Addresses { get; set; }
	public virtual DbSet<Customer> Customers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		_ = modelBuilder.Entity<Address>(entity =>
		{
			_ = entity.Property(e => e.City)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);
			_ = entity.Property(e => e.State)
				.IsRequired()
				.HasMaxLength(2)
				.IsUnicode(false)
				.IsFixedLength();
			_ = entity.Property(e => e.Street)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);
			_ = entity.Property(e => e.ZipCode)
				.IsRequired()
				.HasMaxLength(5)
				.IsUnicode(false)
				.IsFixedLength();

			_ = entity.HasOne(d => d.Customer).WithMany(p => p.Addresses)
				.HasForeignKey(d => d.CustomerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Address_Customer");
		});

		_ = modelBuilder.Entity<Customer>(entity =>
		{
			_ = entity.HasKey(e => e.Id).HasName("PK_Customer");

			_ = entity.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);
			_ = entity.Property(e => e.SocialSecurityNumber)
				.IsRequired()
				.HasMaxLength(9)
				.IsUnicode(false);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
