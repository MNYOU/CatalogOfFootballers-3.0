using Dal.Entities;

namespace Dal.Repositories;

public interface IPlayerRepository : IEntityRepository<Player, long>
{
}