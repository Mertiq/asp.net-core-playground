using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.Services;
using Task = Todo.Core.Entities.Task;

namespace Todo.API;

public class TaskController : CustomBaseController
{
    private readonly IGenericService<Task> _service;

    public TaskController(IGenericService<Task> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tasks = await _service.GetAllAsync();
        return CreateActionResult(CustomResponseDto<List<Task>>.Success(200, tasks));
    }

    /*
    // GET api/<TaskController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<TaskController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<TaskController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<TaskController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
    */
}