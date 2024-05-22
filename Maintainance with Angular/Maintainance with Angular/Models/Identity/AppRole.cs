using Microsoft.AspNetCore.Identity;

namespace Maintainance_with_Angular.Models.Identity
{
	public class AppRole : IdentityRole
	{
		public AppRole()
		{
		}
		public ICollection<AppUserRole> UserRoles { get; set; }
		public bool IsDeleted { get; set; }
		public string DisplayName { get; set; }
	}
}
