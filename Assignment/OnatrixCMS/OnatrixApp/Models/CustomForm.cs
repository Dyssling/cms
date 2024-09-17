using System.ComponentModel.DataAnnotations;

namespace OnatrixApp.Models
{
	public class CustomForm
	{
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Name { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Email { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Phone { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Service { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Message { get; set; } = null!;

	}
}
