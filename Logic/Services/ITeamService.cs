using Dal.Entities;
using Infrastructure.Common;
using Logic.Models.Requests;
using Logic.Models.Responses;

namespace Logic.Services;

public interface ITeamService
{
    public Task<Result<IEnumerable<TeamView>>> GetAllAsync();

    public Task<Result<TeamView>> GetAsync(long id);

    public Task<Result<Team>> GetOrCreateTeamAsync(CreateTeamRequest teamRequest);

    public Task<Result<TeamView>> UpdateAsync(long id, CreateTeamRequest request);
    
    public Task<Result> DeleteAsync(long id);
}