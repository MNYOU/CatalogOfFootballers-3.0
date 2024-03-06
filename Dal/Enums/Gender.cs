using System.ComponentModel.DataAnnotations;

namespace Dal.Enums;

public enum Gender
{
    [Display(Name = "Мужской")]
    Male,
    [Display(Name = "Женский")]
    Female,
}