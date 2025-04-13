using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Bll.Entities;
using ToDoList.Bll.Services;
using ToDoList.Repository.Services;

namespace ToDoList.Project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDOListController : ControllerBase
{
    private readonly IToDoListService _toDoService;
    public ToDOListController(IToDoListService service)
    {
        _toDoService = service;
    }

    [HttpPost("Create")]
    public Task<long> Create([FromBody] ToDoListCreateDto dto)
    {
        return _toDoService.AddToDoListAsync(dto);
    }

    [HttpGet("GetById")]
    public Task<ToDoListGetDto> GetById(long id)
    {
        return _toDoService.GetToTOListByIDAsync(id);
    }

    [HttpGet("GetAll")]
    public Task<List<ToDoListGetDto>> GetAll()
    {
        return _toDoService.GetDoTOListsAsync();
    }

    [HttpPut("Update")]
    public Task Update(ToDoListGetDto dto)
    {
        return _toDoService.UpdateToDoListAsync(dto);
    }

    [HttpDelete("Delete")]
    public Task Delete(long id)
    {
        return _toDoService.DeleteToDOListAsync(id);
    }


}
