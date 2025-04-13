using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Bll.Entities;

public class ToDoListGetDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Discription { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
}
