using System.ComponentModel.DataAnnotations;
using Dal.Enums;
using Infrastructure.Attributes;

namespace Logic.Models.Requests;

public record CreatePlayerRequest
{
    [Display(Name = "Имя")]
    [CustomRequired]
    public string FirstName { get; set; }

    [Display(Name = "Фамилия")]
    [CustomRequired]
    public string LastName { get; set; }
    
    [Display(Name = "Пол")]
    public Gender Gender { get; set; }
    
    [Display(Name = "Команда")]
    [CustomRequired]
    public CreateTeamRequest Team { get; set; }
    
    [UIHint("Date")]
    [Display(Name = "Дата рождения")]
    [CustomRequired]
    public DateOnly?  Birthday { get; set; }
    
    [Display(Name = "Страна")]
    [CustomRequired]
    public Country Country { get; set; }
}