using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitz.Common
{
    
    public static class DBCommon
    {
        const string ConnectionString = "Data Source=(local);Initial Catalog=Project;"
                       + "Integrated Security=true";

        public static SqlConnection GetConnection()
        {
             return new SqlConnection(ConnectionString);
             
        }
    }
}
