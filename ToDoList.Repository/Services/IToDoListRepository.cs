using ToDoList.Dal.Entities;

namespace ToDoList.Repository.Services;

public interface IToDoListRepository
{
    Task<long> AddToDoListAsync(ToDoListEntity toDoList);
    Task<ToDoListEntity> GetToTOListByIDAsync(long id);
    Task<List<ToDoListEntity>> GetDoTOListsAsync(int skip,int take);
    Task UpdateToDoListAsync(ToDoListEntity toDoList);
    Task<List<ToDoListEntity>> SelectByDueDateAsync(DateTime data);
    Task<List<ToDoListEntity>> SelectCompletedAsync(int skip, int take);
    Task<List<ToDoListEntity>> SelectIncompleteAsync(int skip, int take);
    Task DeleteToDOListAsync(long id);
    int GetToDoListCount();
}