using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Maintainance_with_Angular.Extensions
{
	public static class IdentityErrorExtension
	{
		public static string ToErrorString(this IEnumerable<IdentityError> errors)
		{
			var error = new StringBuilder();
			foreach (var item in errors)
			{
				error.Append($"Error - {item.Description} ");
			}
			return error.ToString();
		}
	}
}
