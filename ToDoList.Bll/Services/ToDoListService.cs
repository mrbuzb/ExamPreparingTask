using AutoMapper;
using FluentValidation;
using System;
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

    public async Task<List<ToDoListGetDto>> GetDoTOListsAsync()
    {
        var res  = await _toDoListRepository.GetDoTOListsAsync();
        return (List<ToDoListGetDto>)res.Select(tdl => _mapper.Map<ToDoListGetDto>(tdl));
    }

    public async Task<ToDoListGetDto> GetToTOListByIDAsync(long id)
    {
        return _mapper.Map<ToDoListGetDto>(await _toDoListRepository.GetToTOListByIDAsync(id));
    }

    public async Task UpdateToDoListAsync(ToDoListGetDto toDoList)
    {
        await _toDoListRepository.UpdateToDoListAsync(_mapper.Map<ToDoListEntity>(toDoList));
    }
}
