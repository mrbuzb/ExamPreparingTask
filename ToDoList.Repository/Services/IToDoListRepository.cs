using ToDoList.Dal.Entities;

namespace ToDoList.Repository.Services;

public interface IToDoListRepository
{
    Task<long> AddToDoListAsync(ToDoListEntity toDoList);
    Task<ToDoListEntity> GetToTOListByIDAsync(long id);
    Task<List<ToDoListEntity>> GetDoTOListsAsync();
    Task UpdateToDoListAsync(ToDoListEntity toDoList);
    Task DeleteToDOListAsync(long id);
}