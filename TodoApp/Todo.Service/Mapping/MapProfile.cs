using AutoMapper;
using Todo.Core.DTOs;
using Task = Todo.Core.Entities.Task;

namespace Todo.Service;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Task, TaskDto>().ReverseMap();
    }
}