using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5, ErrorMessage = "minimum length is 5")]
		public string Password { get; set; }
		
		public bool RemmberMe { get; set; }
	}
}
