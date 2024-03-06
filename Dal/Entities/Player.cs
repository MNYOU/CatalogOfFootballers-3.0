using Dal.Enums;

namespace Dal.Entities;

public class Player : Entity<long>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Gender Gender { get; set; }

    public DateOnly Birthday { get; set; }

    public Country Country { get; set; }
    
    public Team Team { get; set; }
}