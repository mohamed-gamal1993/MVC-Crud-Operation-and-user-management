using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
	public class ResetpasswordViewModel
	{
		public string Email { get; set; }
		public string Token { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5, ErrorMessage = "minimum length is 5")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare("NewPassword", ErrorMessage = "password dosn't match")]
		public string ConfirmPassword { get; set; }
	}
}
