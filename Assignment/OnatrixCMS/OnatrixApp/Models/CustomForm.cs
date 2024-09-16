using System.ComponentModel.DataAnnotations;

namespace OnatrixApp.Models
{
	public class CustomForm
	{
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? FullName { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		[RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$")]
		public string? Email { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		[RegularExpression("^[0-9*#+ .()-]{7,}$")]
		public string? Phone { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Service { get; set; } = null!;

		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string? Message { get; set; } = null!;

	}
}
