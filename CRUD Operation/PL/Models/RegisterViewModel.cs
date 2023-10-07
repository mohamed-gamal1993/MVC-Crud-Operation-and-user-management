using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="Email is required")]
		[EmailAddress(ErrorMessage ="Invaild Email")]
		public string Email { get; set; }
		[Required(ErrorMessage ="Password is Required")]
		[MinLength(5,ErrorMessage ="minimum length is 5")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare("Password",ErrorMessage ="password dosn't match")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}
