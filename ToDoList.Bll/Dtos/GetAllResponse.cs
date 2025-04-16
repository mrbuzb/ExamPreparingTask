using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Bll.Entities;

namespace ToDoList.Bll.Dtos;

public class GetAllResponse
{
    public long Count { get; set; }
    public List<ToDoListGetDto> Dtos { get; set; }
}
