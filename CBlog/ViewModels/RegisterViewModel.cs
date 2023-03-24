using System.ComponentModel.DataAnnotations;

namespace CBlog.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Date of birth is required")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		[DataType(DataType.Password)]
		public string RepeatPassword { get; set; }
	}
}
