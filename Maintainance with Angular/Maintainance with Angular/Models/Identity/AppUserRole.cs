using Microsoft.AspNetCore.Identity;

namespace Maintainance_with_Angular.Models.Identity
{
	public class AppUserRole : IdentityUserRole<string>
	{
		public string UserId { get; set; }
		public virtual AppUser User { get; set; }
		public string RoleId { get; set; }
		public virtual AppRole Role { get; set; }
	}
}
