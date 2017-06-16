using System.Data;
using System.Data.SqlClient;
using Band_Tracker;

namespace Band_Tracker.Objects
{
  public class DB
  {
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
