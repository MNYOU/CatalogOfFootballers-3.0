namespace Dal.Entities;

public class Team : Entity<long>
{
    public string Name { get; set; }

    public IList<Player> Footballers { get; set; }
}