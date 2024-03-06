using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Attributes;

public class CustomRequiredAttribute : RequiredAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return $"Поле '{name}' обязательно!";
    }
}