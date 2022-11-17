using Blitz.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitz.Common
{
    
    public static  class DBCommon
    {
        const string ConnectionString = "Data Source=(local);Initial Catalog=Project;"
                       + "Integrated Security=true";

        public static SqlConnection GetConnection()
        {
             return new SqlConnection(ConnectionString);
             
        }
        public static int ExecuteNonQuerySql(string cmdText,
            List<SqlParameter> parameters = null,
            CommandType commandType = CommandType.Text)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.CommandType = commandType;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                result = command.ExecuteNonQuery();

                if (command.Parameters.Contains("@successStatus"))
                {
                    var status = command.Parameters["@successStatus"].Value.ToString();
                    return Convert.ToInt32(status.ToLower() == "true" ? 1 : 0);
                }
            }
            return result;
        }


        public static DataTable GetResultDataTableBySql(string cmdText,
                                                List<SqlParameter> parameters = null,
                                                CommandType commandType = CommandType.Text)
        {
            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                result.Load(reader);
                foreach (DataColumn column in result.Columns) column.ReadOnly = false;
            }
            return result;
        }
    }
}
