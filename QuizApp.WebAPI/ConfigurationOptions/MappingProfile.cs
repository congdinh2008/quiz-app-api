using AutoMapper;
using QuizApp.Business;
using QuizApp.Models;

namespace QuizApp.WebAPI;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Quiz, QuizViewModel>().ReverseMap();
        CreateMap<Quiz, QuizCreateUpdateCommand>().ReverseMap();
    }
}
