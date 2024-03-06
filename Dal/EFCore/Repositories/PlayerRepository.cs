using Dal.Entities;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dal.EFCore.Repositories;

public class PlayerRepository : EntityRepository<Player, long>, IPlayerRepository
{
    public PlayerRepository(DataContext context) : base(context, context.Footballers)
    {
    }

    public override async Task<Player?> FindAsync(long id)
    {
        return await Items
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}