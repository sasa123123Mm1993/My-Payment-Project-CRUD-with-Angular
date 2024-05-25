using Maintainance_with_Angular.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Maintainance_with_Angular.Models
{
	public class PaymentDetailContext : IdentityDbContext<AppUser,
							AppRole,
							string,
							IdentityUserClaim<string>,
							AppUserRole,
							IdentityUserLogin<string>,
							IdentityRoleClaim<string>,
							IdentityUserToken<string>>
	{
		public PaymentDetailContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<PaymentDetail> PaymentDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// builder.Seed();
			builder.Entity<AppRole>().HasMany(ur => ur.UserRoles)
						   .WithOne(u => u.Role)
						   .HasForeignKey(ur => ur.RoleId)
						   .IsRequired();

			builder.Entity<AppUser>().HasMany(ur => ur.UserRoles)
						.WithOne(u => u.User)
						.HasForeignKey(ur => ur.UserId)
						.IsRequired();

			builder.Entity<AppUserRole>().HasKey(s => new { s.UserId, s.RoleId });
		}
	}
}
