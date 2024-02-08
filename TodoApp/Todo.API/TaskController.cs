using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.DTOs;
using Todo.Core.Services;
using Task = Todo.Core.Entities.Task;

namespace Todo.API;

public class TaskController : CustomBaseController
{
    private readonly IGenericService<Task> _service;
    private readonly IMapper _mapper;

    public TaskController(IGenericService<Task> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tasks = await _service.GetAllAsync();
        return CreateActionResult(CustomResponseDto<List<Task>>.Success(200, tasks));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _service.GetByIdAsync(id);
        return CreateActionResult(CustomResponseDto<Task>.Success(200, task));
    }

    [HttpPost]
    public async Task<IActionResult> Post(TaskDto taskDto)
    {
        var task = _mapper.Map<Task>(taskDto);
        await _service.AddAsync(task);
        return CreateActionResult(CustomResponseDto<Task>.Success(201, task));
    }

    [HttpPut]
    public async Task<IActionResult> Put(Task task)
    {
        await _service.UpdateAsync(task);
        return CreateActionResult(CustomResponseDto<Task>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _service.GetByIdAsync(id);
        await _service.DeleteAsync(task);
        return CreateActionResult(CustomResponseDto<Task>.Success(204));
    }
    
}