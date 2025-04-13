using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Dal;
using ToDoList.Dal.Entities;
using ToDoList.Repository.Settings;

namespace ToDoList.Repository.Services;

public class ToDoListRepository : IToDoListRepository
{
    private readonly string  _connection;
    public ToDoListRepository(SqlDBConeectionString connection)
    {
        _connection = connection.ConnectionString;
    }

    public async Task<long> AddToDoListAsync(ToDoListEntity toDoList)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd =new SqlCommand("AddToDoList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", toDoList.Title);
                cmd.Parameters.AddWithValue("@Discription", toDoList.Discription);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoList.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoList.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoList.DueDate);

                return (long)await cmd.ExecuteScalarAsync();
            }
        }
    }

    public async Task DeleteToDOListAsync(long id)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("DeleteToDOList", conn))
            {
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id",id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public async Task<List<ToDoListEntity>> GetDoTOListsAsync()
    {
        var ToDoLists = new List<ToDoListEntity>();
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetDoTOLists", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ToDoLists.Add(new ToDoListEntity
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Discription = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5)
                        });
                    }
                }
            }
        }
        return ToDoLists;
    }

    public async Task<ToDoListEntity> GetToTOListByIDAsync(long id)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetToTOListByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    return new ToDoListEntity
                    {
                        Id = reader.GetInt64(0),
                        Title = reader.GetString(1),
                        Discription = reader.GetString(2),
                        IsCompleted = reader.GetBoolean(3),
                        CreatedAt = reader.GetDateTime(4),
                        DueDate = reader.GetDateTime(5)
                    };
                }
            }
        }
    }

    public async Task UpdateToDoListAsync(ToDoListEntity toDoList)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("UpdateToDoList",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", toDoList.Title);
                cmd.Parameters.AddWithValue("@Discription", toDoList.Discription);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoList.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoList.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoList.DueDate);

                cmd.ExecuteNonQuery();
            }
        }   
    }
}
