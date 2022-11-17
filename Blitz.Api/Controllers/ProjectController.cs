using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blitz.DataAccess;
using Blitz.Models;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Blitz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectDAO projectDAO = new ProjectDAO();

        [HttpGet]
        [Route("/GetAll")]//get all projects its name,id,and startdate
        public IEnumerable<Project> Get()
        {
            return projectDAO.GetProjects();

        }

        [HttpGet]
        [Route("/ProjectByID/{id}")]//get project by given id
        public IEnumerable<Project> ByID(int id)
        {
            return (IEnumerable<Project>)projectDAO.GetProjectById(id);
        }


        [HttpDelete]
        [Route("/Delete/{id}")]//delete the project by given id
        public int delete(int id)
        {
            return projectDAO.DeleteProject(id);
        }

        [HttpPost]
        [Route("/AddProject")]//add the new project 
        public int AddProject(Project p)
        {
            return projectDAO.AddProject(p);
        }

        [HttpPut]
        [Route("/UpdateProject")]//update the project by its id
        public int UpdateProject(Project p)
        {
            return projectDAO.UpdateProject(p);
        }

    }
}
