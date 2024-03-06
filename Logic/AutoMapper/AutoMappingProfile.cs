using AutoMapper;
using Dal.Entities;
using Logic.Models.Requests;
using Logic.Models.Responses;

namespace Logic.AutoMapper;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<CreatePlayerRequest, Player>().ReverseMap();
        CreateMap<Player, PlayerView>();

        CreateMap<CreateTeamRequest, Team>().ReverseMap();
        CreateMap<Team, TeamView>();
    }
}