using Infrastructure.Common;
using Logic.Models.Requests;
using Logic.Models.Responses;

namespace Logic.Services;

public interface IPlayerService
{
    public Task<Result<IEnumerable<PlayerView>>> GetAllAsync();

    public Task<Result<PlayerView>> GetAsync(long id);

    public Task<Result<CreatePlayerRequest>> GetAsRequestAsync(long id);

    public Task<Result<PlayerView>> CreateAsync(CreatePlayerRequest request);

    public Task<Result<PlayerView>> UpdateAsync(long id, CreatePlayerRequest request);

    public Task<Result> DeleteAsync(long id);
}