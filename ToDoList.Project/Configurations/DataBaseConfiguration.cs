using ToDoList.Dal;
using ToDoList.Repository.Settings;

namespace ToDoList.Project.Configurations;

public static class DataBaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        var sqlDBConeectionString = new SqlDBConeectionString(connectionString);
        builder.Services.AddSingleton<SqlDBConeectionString>(sqlDBConeectionString);
    }
}
