using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }
	}
}
