using ToDoList.Bll.Dtos;
using ToDoList.Bll.Entities;

namespace ToDoList.Bll.Services;

public interface IToDoListService
{
    Task<long> AddToDoListAsync(ToDoListCreateDto toDoList);
    Task<ToDoListGetDto> GetToTOListByIDAsync(long id);
    Task<GetAllResponse> GetDoTOListsAsync(int skip,int take);
    Task UpdateToDoListAsync(ToDoListGetDto toDoList);
    Task<GetAllResponse> SelectByDueDateAsync(DateTime data);
    Task<GetAllResponse> SelectCompletedAsync(int skip,int take);
    Task<GetAllResponse> SelectIncompleteAsync(int skip,int take);
    Task DeleteToDOListAsync(long id);
}