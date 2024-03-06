using Dal.EFCore;
using Dal.EFCore.Repositories;
using Dal.Repositories;
using Logic.AutoMapper;
using Logic.Services;

namespace WebApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddDbContext<DataContext>();
        services.AddAutoMapper(conf => conf.AddProfile<AutoMappingProfile>());

        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.MapControllers();
        app.MapFallbackToController("RouteNotFounded", "Home");
    }
}