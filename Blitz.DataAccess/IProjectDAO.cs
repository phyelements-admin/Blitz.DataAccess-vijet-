using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blitz.Models;

namespace Blitz.DataAccess
{
    public interface IProjectDAO
    {

        List<Project> GetProjects();//to get all the project present in databse
        IEnumerable<Project> GetProjectById(int Id);//to get the specific project by id given
        int AddProject(Project p);//to add a new project
        int UpdateProject(Project p);//update the existing project
        int DeleteProject(int Id);//delte the project by its id
    }
}
