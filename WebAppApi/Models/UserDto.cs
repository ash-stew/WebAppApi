using System.ComponentModel.DataAnnotations;

namespace WebAppApi.Models
{
	public class UserDto
	{
		[Required]
		public string Firstname { get; set; } = "";
		[Required(ErrorMessage = "Please provide your Lastname")]
		public string Lastname { get; set; } = "";
		[Required]
		[EmailAddress]
		public string Email { get; set; } = "";
		public string Phone { get; set; } = "";
		[Required]
		[MinLength(5, ErrorMessage = "The address should be atleast 5 characters")]
		[MaxLength(1000, ErrorMessage = "The address should be less than 1000 characters")]
		public string Address { get; set; } = "";

	}
}
