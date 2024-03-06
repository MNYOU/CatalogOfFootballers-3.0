using System.ComponentModel.DataAnnotations;
using Dal.Enums;

namespace Logic.Models.Responses;

public record PlayerView
{
    public long Id { get; set; }
    
    [Display(Name = "Имя")]
    public string FirstName { get; set; }

    [Display(Name = "Фамилия")]
    public string LastName { get; set; }
    
    [Display(Name = "Пол")]
    public Gender Gender { get; set; }
    
    [Display(Name = "Команда")]
    public TeamView Team { get; set; }
    
    [UIHint("Date")]
    [Display(Name = "Дата рождения")]
    public DateOnly?  Birthday { get; set; }
    
    [Display(Name = "Страна")]
    public Country Country { get; set; }
}