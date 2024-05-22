using Microsoft.AspNetCore.Identity;

namespace Maintainance_with_Angular.Models.Identity
{
	public class AppUser : IdentityUser
	{
		public AppUser()
		{
			UserRoles = new List<AppUserRole>();
		}
		public ICollection<AppUserRole> UserRoles { get; set; }
		public bool IsDeleted { get; set; }

	}
}
