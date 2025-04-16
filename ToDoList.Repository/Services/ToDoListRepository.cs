using Microsoft.Data.SqlClient;
using System.Data;
using ToDoList.Dal.Entities;
using ToDoList.Repository.Settings;

namespace ToDoList.Repository.Services;

public class ToDoListRepository : IToDoListRepository
{
    private readonly string _connection;
    public ToDoListRepository(SqlDBConeectionString connection)
    {
        _connection = connection.ConnectionString;
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



    public async Task<long> AddToDoListAsync(ToDoListEntity toDoList)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("AddToDoList", conn))
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
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public async Task<List<ToDoListEntity>> GetDoTOListsAsync(int skip, int take)
    {
        if (skip < 0) skip = 0;
        if (take < 0 || take > 100) take = 100;
        var ToDoLists = new List<ToDoListEntity>();
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetToDoListsPagenation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Offset", skip);
                cmd.Parameters.AddWithValue("@PageSize", take);
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

    public int GetToDoListCount()
    {
        var Counts = 0;
        using(var conn = new SqlConnection(_connection))
        {
             conn.Open();
            using (var cmd = new SqlCommand("SELECT dbo.CountOfToDoLists()", conn))
            {
                cmd.CommandType = CommandType.Text;
                Counts = (int) cmd.ExecuteScalar();
            }
        }
        return Counts;
    }

    public async Task<ToDoListEntity> GetToTOListByIDAsync(long id)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetToTOListByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
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
        throw new Exception("ToDoItemNotFound");
    }

    public async Task<List<ToDoListEntity>> SelectByDueDateAsync(DateTime data)
    {
        var ToDoLists = new List<ToDoListEntity>();
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("GetToDoListByDueDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DueDate", data);
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

    public async Task<List<ToDoListEntity>> SelectCompletedAsync(int skip, int take)
    {
        if (skip < 0) skip = 0;
        if (take < 0 || take > 100) take = 100;

        var ToDoLists = new List<ToDoListEntity>();
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("SelectAllCompletedToDoListPagenation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Offset", skip);
                cmd.Parameters.AddWithValue("@PageSize", take);
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

    public async Task<List<ToDoListEntity>> SelectIncompleteAsync(int skip, int take)
    {
        if (skip < 0) skip = 0;
        if (take < 0 || take > 100) take = 100;
        var ToDoLists = new List<ToDoListEntity>();
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();
            using (var cmd = new SqlCommand("SelectAllInCompletedToDoListPagenation", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Offset", skip);
                cmd.Parameters.AddWithValue("@PageSize", take);
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

    public async Task UpdateToDoListAsync(ToDoListEntity toDoList)
    {
        using (var conn = new SqlConnection(_connection))
        {
            await conn.OpenAsync();

            using (var cmd = new SqlCommand("UpdateToDoList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", toDoList.Title);
                cmd.Parameters.AddWithValue("@Id", toDoList.Id);
                cmd.Parameters.AddWithValue("@Discription", toDoList.Discription);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoList.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoList.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoList.DueDate);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
