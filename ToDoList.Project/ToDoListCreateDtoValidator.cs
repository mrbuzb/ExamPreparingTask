using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Bll.Entities;

namespace ToDoList.Bll.Validators;

public class ToDoListCreateDToValidator : AbstractValidator<ToDoListCreateDto>
{
    public ToDoListCreateDToValidator()
    {
        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("ToDoList Title  is required.")
                .Length(1,200).WithMessage("ToDoList Title must be between 1 and 200 characters.");
        RuleFor(x => x.Discription)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 1000).WithMessage("Description must be between 10 and 200 characters.");
    }
}
