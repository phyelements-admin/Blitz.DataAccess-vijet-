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
          
            using (SqlConnection connection =DBCommon.GetConnection())  
                
            {
                SqlCommand command = new SqlCommand();
                command.CommandType= CommandType.Text;
                command.CommandText = "INSERT INTO Project_Table (Id,Name,StartDate) Values (@Id,@Name,@StartDate)";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", p.Id);
                command.Parameters.AddWithValue("@Name", p.Name);
                command.Parameters.AddWithValue("@StartDate", p.StartDate);

                connection.Open();
                return command.ExecuteNonQuery();
                
            }

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
        public IEnumerable<Project> GetProjects()
        {
            using (SqlConnection connection = DBCommon.GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = " SELECT * FROM Project_Table; ";
                command.Connection = connection;
                
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
