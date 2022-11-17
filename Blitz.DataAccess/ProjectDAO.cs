using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Blitz.Common;
using Blitz.Models;
using Newtonsoft.Json;


namespace Blitz.DataAccess
{
    public class ProjectDAO : IProjectDAO
    {
        //Function to add a new project
        public int AddProject(Project p)
        {

            //return DBCommon.ExecuteNonQuerySql(" INSERT INTO Project_Table (ID,Name,StartDate) VALUES (10,'Vijet','2021 - 11 - 17 06:04:40.433'); ");
            return DBCommon.ExecuteNonQuerySql("DELETE from Project_Table where Id=10;");


        }

        //Function to  Delete the existing project 
        public int DeleteProject(int Id)//to change
        {

            using (SqlConnection connection = DBCommon.GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM Project_Table WHERE Id=@ID ;";
                command.Connection = connection;
                command.Parameters.AddWithValue("@Id", Id);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        //function to get project by id given in params
        public IEnumerable<Project> GetProjectById(int Id)
        {
            
            using (SqlConnection connection = DBCommon.GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Project_Table WHERE Id=@Id;";
                command.Connection = connection;
                command.Parameters.AddWithValue("@Id", Id);
                

                List<Project> prj = new List<Project>();
                connection.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        prj.Add(new Project
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Name = sdr["Name"].ToString(),
                            StartDate = (DateTime)sdr["StartDate"]
                        });
                    }
                }
                connection.Close(); 
                return prj;
            }
        }

        //functions to display all the project avialable in table 
        public List<Project> GetProjects()
        {
            var data_t = DBCommon.GetResultDataTableBySql("SELECT * FROM Project_Table;");
            List<Project> prj = new List<Project>();
            for (int i = 0; i < data_t.Rows.Count; i++)
            {
                Project project = new Project();
                project.Id = Convert.ToInt32(data_t.Rows[i]["Id"]);
                project.Name = Convert.ToString(data_t.Rows[i]["Name"]);
                project.StartDate = Convert.ToDateTime(data_t.Rows[i]["StartDate"]);
                prj.Add(project);
            }
            return prj;
        }
            //function to update the existing project by id given
            public int UpdateProject(Project p)
        {
            using (SqlConnection connection = DBCommon.GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE  Project_Table SET Name =@Name,StartDate=@StartDate where Id=@Id";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", p.Id);
                command.Parameters.AddWithValue("@Name", p.Name);
                command.Parameters.AddWithValue("@StartDate", p.StartDate);

                connection.Open();
                return  command.ExecuteNonQuery();
            }

        }

    }
}
