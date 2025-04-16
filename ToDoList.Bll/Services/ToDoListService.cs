using AutoMapper;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Bll.Entities;
using ToDoList.Dal.Entities;
using ToDoList.Repository.Services;

namespace ToDoList.Bll.Services;

public class ToDoListService : IToDoListService
{
    private readonly IToDoListRepository _toDoListRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ToDoListCreateDto> _validator;
    public ToDoListService(IToDoListRepository toDoListRepository, IMapper mapper, IValidator<ToDoListCreateDto> validator)
    {
        _toDoListRepository = toDoListRepository;
        _mapper = mapper;
        _validator = validator;
    }



//    int pageNumber = 2; // qaysi sahifa kerak
//    int pageSize = 10;  // har bir sahifada nechta element bo‘ladi
//    int offset = (pageNumber - 1) * pageSize;

//    string query = @"SELECT * FROM Students
//                 ORDER BY StudentId
//                 OFFSET @Offset ROWS
//                 FETCH NEXT @PageSize ROWS ONLY;";

//using (SqlConnection connection = new SqlConnection("your_connection_string"))
//using (SqlCommand command = new SqlCommand(query, connection))
//{
//    command.Parameters.AddWithValue("@Offset", offset);
//    command.Parameters.AddWithValue("@PageSize", pageSize);

//    connection.Open();
//    using (SqlDataReader reader = command.ExecuteReader())
//    {
//        while (reader.Read())
//        {
//            // Ma'lumotlarni o'qish
//            Console.WriteLine(reader["StudentName"]);
//        }
//    }
//}






    public async Task<long> AddToDoListAsync(ToDoListCreateDto toDoList)
    {
        var status = _validator.Validate(toDoList);
        if (!status.IsValid)
        {
            throw new Exception("toDoList is not Valid");
        }
        var entity = _mapper.Map<ToDoListEntity>(toDoList);
        entity.CreatedAt = DateTime.Now;
        entity.IsCompleted = false;
        return await _toDoListRepository.AddToDoListAsync(entity);
    }

    public async Task DeleteToDOListAsync(long id)
    {
        await _toDoListRepository.DeleteToDOListAsync(id);
    }

    public async Task<List<ToDoListGetDto>> GetDoTOListsAsync(int skip, int take)
    {
        var res = await _toDoListRepository.GetDoTOListsAsync(skip, take);
        var mappedResult = res.Select(tdl => _mapper.Map<ToDoListGetDto>(tdl)).ToList();
        return mappedResult;
    }


    public async Task<ToDoListGetDto> GetToTOListByIDAsync(long id)
    {
        return _mapper.Map<ToDoListGetDto>(await _toDoListRepository.GetToTOListByIDAsync(id));
    }

    public async Task UpdateToDoListAsync(ToDoListGetDto toDoList)
    {
        await _toDoListRepository.UpdateToDoListAsync(_mapper.Map<ToDoListEntity>(toDoList));


        object salom =1;
        salom = 'a';
    }
}
