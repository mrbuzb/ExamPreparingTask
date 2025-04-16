using ToDoList.Bll.Entities;

namespace ToDoList.Bll.Services;

public interface IToDoListService
{
    Task<long> AddToDoListAsync(ToDoListCreateDto toDoList);
    Task<ToDoListGetDto> GetToTOListByIDAsync(long id);
    Task<List<ToDoListGetDto>> GetDoTOListsAsync(int skip,int take);
    Task UpdateToDoListAsync(ToDoListGetDto toDoList);
    Task DeleteToDOListAsync(long id);
}