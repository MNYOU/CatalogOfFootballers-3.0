using System.ComponentModel.DataAnnotations;

namespace Logic.Models.Responses;

public record TeamView
{
    public long Id { get; set; }
    
    [Display(Name = "Название команды")]
    public string Name { get; set; }
}