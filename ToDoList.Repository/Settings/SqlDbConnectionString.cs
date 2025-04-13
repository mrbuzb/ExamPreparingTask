using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Repository.Settings;

public class SqlDBConeectionString
{
    private string connectionString;

    public string ConnectionString
    {
        get { return connectionString; }
        set { connectionString = value; }
    }

    public SqlDBConeectionString(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
