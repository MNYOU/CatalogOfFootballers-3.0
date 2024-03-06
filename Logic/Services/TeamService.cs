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

public class TeamService : ITeamService
{
    private readonly IMapper mapper;
    private readonly ITeamRepository repository;
    private readonly ILogger<Team> logger;

    public TeamService(IMapper mapper, ITeamRepository repository, ILogger<Team> logger)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<Result<IEnumerable<TeamView>>> GetAllAsync()
    {
        var teams = repository
            .GetAll()
            .OrderBy(t => t.Name)
            .Select(t => mapper.Map<TeamView>(t))
            .AsEnumerable();

        return teams.AsResult();
    }

    public async Task<Result<TeamView>> GetAsync(long id)
    {
        return (await GetByIdAsync(id))
            .Then(p => mapper.Map<TeamView>(p));
    }

    public async Task<Result<TeamView>> GetViewAsync(long id)
    {
        return (await GetByIdAsync(id))
            .Then(p => mapper.Map<TeamView>(p));
    }

    public async Task<Result<Team>> GetOrCreateTeamAsync(CreateTeamRequest teamRequest)
    {
        var team = await GetByNameAsync(teamRequest.Name);
        if (!team.IsSuccess)
        {
            team = await CreateAsync(teamRequest);
        }

        return team;
    }


    public async Task<Result<TeamView>> UpdateAsync(long id, CreateTeamRequest request)
    {
        var getResult = await GetByIdAsync(id);
        if (!getResult.IsSuccess)
            return Result.Fail<TeamView>(getResult.Error);

        var team = getResult.Value;
        mapper.Map(request, team);
        repository.Update(team);
        try
        {
            await repository.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Произошла ошибка: {ErrorMessage}", e.Message);
            return Result.Fail<TeamView>("Ошибка при обновлении данных команды: " + e.Message);
        }

        return mapper.Map<TeamView>(team);
    }

    public async Task<Result> DeleteAsync(long id)
    {
        var getResult = await GetByIdAsync(id);
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
            return Result.Fail("Ошибка при удалении игрока" + e.Message);
        }

        return Result.Ok();
    }

    private async Task<Result<Team>> GetByIdAsync(long id)
    {
        var team = await repository
            .GetAll()
            .FirstOrDefaultAsync(p => p.Id == id);

        return team ?? Result.Fail<Team>($"Команды с \"{nameof(id)}\": {id} не существует.");
    }

    private async Task<Result<Team>> CreateAsync(CreateTeamRequest request)
    {
        var team = mapper.Map<Team>(request);
        repository.Add(team);
        try
        {
            await repository.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            logger.LogError(e, "Произошла ошибка: {ErrorMessage}", e.Message);
            return Result.Fail<Team>("Ошибка при сохранении данных команды: " + e.Message);
        }

        return team;
    }

    private async Task<Result<Team>> GetByNameAsync(string name)
    {
        var team = await repository
            .GetAll()
            .FirstOrDefaultAsync(t => t.Name == name);

        return team ?? Result.Fail<Team>($"Команды с таким \"{nameof(name)}\" не существует.");
    }
}