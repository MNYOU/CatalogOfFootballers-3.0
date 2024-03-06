using Logic.Models.Requests;
using Logic.Models.Responses;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("players")]
public class PlayerController : Controller
{
    private readonly IPlayerService playerService;
    private readonly ITeamService teamService;

    public PlayerController(IPlayerService playerService, ITeamService teamService)
    {
        this.playerService = playerService;
        this.teamService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await playerService.GetAllAsync();

        return result.IsSuccess
            ? View(result.Value)
            : GetErrorView(result.Error);
    }


    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var teamsResult = await teamService.GetAllAsync();
        if (!teamsResult.IsSuccess)
            return GetErrorView(teamsResult.Error);

        ViewData["Teams"] = teamsResult.Value;

        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] CreatePlayerRequest model)
    {
        if (ModelState.IsValid)
        {
            var result = await playerService.CreateAsync(model);
            return result.IsSuccess
                ? View("Success")
                : GetErrorView(result.Error);
        }

        var teamsResult = await teamService.GetAllAsync();
        if (!teamsResult.IsSuccess)
            return GetErrorView(teamsResult.Error);

        ViewData["Teams"] = teamsResult.Value;

        return View(model);
    }

    [HttpGet("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id)
    {
        var player = await playerService.GetAsRequestAsync(id);
        if (!player.IsSuccess)
            return GetErrorView(player.Error);

        var teamsResult = await teamService.GetAllAsync();
        if (!teamsResult.IsSuccess)
            return GetErrorView(teamsResult.Error);

        ViewData["Teams"] = teamsResult.Value;

        return View(player.Value);
    }

    [HttpPost("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromForm] CreatePlayerRequest model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await playerService.UpdateAsync(id, model);

        return result.IsSuccess
            ? RedirectToAction(nameof(GetAll))
            : GetErrorView(result.Error);
    }

    [HttpPost("delete/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await playerService.DeleteAsync(id);
        return result.IsSuccess
            ? RedirectToAction(nameof(GetAll))
            : GetErrorView(result.Error);
    }

    private ViewResult GetErrorView(string message)
    {
        return View("Error", new ErrorViewModel(message));
    }
}