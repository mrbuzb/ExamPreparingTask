
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using ToDoList.Bll.Entities;
using ToDoList.Bll.Mappers;
using ToDoList.Bll.Services;
using ToDoList.Bll.Validators;
using ToDoList.Dal;
using ToDoList.Project.Configurations;
using ToDoList.Repository.Services;
using ToDoList.Repository.Settings;

namespace ToDoList.Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.ConfigureDatabase();

            builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();
            builder.Services.AddScoped<IToDoListService, ToDoListService>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddValidatorsFromAssemblyContaining<ToDoListCreateDto>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
