using System.ComponentModel.DataAnnotations;
using Infrastructure.Attributes;

namespace Logic.Models.Requests;

public record CreateTeamRequest
{
    [Display(Name = "Команда")]
    [CustomRequired]
    public string Name { get; set; }
}
