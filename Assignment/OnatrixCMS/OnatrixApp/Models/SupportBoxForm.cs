using System.ComponentModel.DataAnnotations;

namespace OnatrixApp.Models
{
    public class SupportBoxForm
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SupportBoxEmail { get; set; } = null!;
    }
}
