using System.ComponentModel.DataAnnotations;

namespace Dal.Enums;

public enum Country
{
    [Display(Name = "Россия")]
    Russia,
    [Display(Name = "США")]
    USA,
    [Display(Name = "Италия")]
    Italy,
}