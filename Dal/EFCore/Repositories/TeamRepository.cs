using Dal.Entities;
using Dal.Repositories;

namespace Dal.EFCore.Repositories;

public class TeamRepository : EntityRepository<Team, long>, ITeamRepository
{
    public TeamRepository(DataContext context) : base(context, context.Teams)
    {
    }
}