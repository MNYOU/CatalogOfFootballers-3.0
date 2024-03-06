using AutoMapper;
using Dal.Entities;
using Dal.Repositories;
using Infrastructure.Common;
using Infrastructure.Extensions;
using Logic.Models.Requests;
using Logic.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logic.Services;

public class PlayerService : IPlayerService
{
    private readonly IMapper mapper;
    private readonly IPlayerRepository repository;
    private readonly ITeamService teamService;
    private readonly ILogger<Player> logger;

    public PlayerService(IMapper mapper, IPlayerRepository repository, ITeamService teamService, ILogger<Player> logger)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.teamService = teamService;
        this.logger = logger;
    }

    public async Task<Result<IEnumerable<PlayerView>>> GetAllAsync()
    {
        var players = repository
            .GetAll()
            .OrderBy(p => p.FirstName)
            .Include(p => p.Team)
            .Select(p => mapper.Map<PlayerView>(p))
            .AsEnumerable();

        return players.AsResult();
    }


    public async Task<Result<PlayerView>> GetAsync(long id)
    {
        return (await GetPlayerAsync(id))
            .Then(p => mapper.Map<PlayerView>(p));
    }

    public async Task<Result<PlayerView>> CreateAsync(CreatePlayerRequest request)
    {
        var team = await teamService.GetOrCreateTeamAsync(request.Team);
        if (!team.IsSuccess)
            return Result.Fail<PlayerView>(team.Error);

        var player = mapper.Map<Player>(request);
        player.Team = team.Value;
        repository.Add(player);
        try
        {
            await repository.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Произошла ошибка: {ErrorMessage}", e.Message);
            return Result.Fail<PlayerView>("Произошла ошибка при сохранении данных игрока: " + e.Message);
        }

        return mapper.Map<PlayerView>(player);
    }

    public async Task<Result<PlayerView>> UpdateAsync(long id, CreatePlayerRequest request)
    {
        var team = await teamService.GetOrCreateTeamAsync(request.Team);
        if (!team.IsSuccess)
            return Result.Fail<PlayerView>(team.Error);

        var player = mapper.Map<Player>(request);
        player.Id = id;
        player.Team = team.Value;

        repository.Update(player);
        try
        {
            await repository.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Произошла ошибка: {ErrorMessage}", e.Message);
            return Result.Fail<PlayerView>("Произошла ошибка при обновлении данных игрока: " + e.Message);
        }

        return mapper.Map<PlayerView>(player);
    }

    public async Task<Result> DeleteAsync(long id)
    {
        var getResult = await GetPlayerAsync(id);
        if (!getResult.IsSuccess)
            return getResult;

        repository.Delete(getResult.Value);
        try
        {
            await repository.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Произошла ошибка: {ErrorMessage}", e.Message);
            return Result.Fail("Ошибка при удалении игрока: " + e.Message);
        }

        return Result.Ok();
    }

    public async Task<Result<CreatePlayerRequest>> GetAsRequestAsync(long id)
    {
        return (await GetPlayerAsync(id))
            .Then(p => mapper.Map<CreatePlayerRequest>(p));
    }

    private async Task<Result<Player>> GetPlayerAsync(long id)
    {
        var player = await repository.FindAsync(id);
        return player ?? Result.Fail<Player>($"Команды с \"{nameof(id)}\": {id} не существует.");
    }
}