using System.ComponentModel.DataAnnotations;

namespace CBlog.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
