using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.AspNetCore.Authorization;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DaisyContext _context;

        public ProjectController(DaisyContext context)
        {
            _context = context;
        }
        // GET: api/Projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProject()
        {
            var query = from project in _context.Set<Project>()
                        select project;
            return query.OrderBy(q => q.StartTime).ToList();
        }
        // GET: api/Projects/5
        [HttpGet,Route("search")]
        public ActionResult<IEnumerable<Project>> SearchProject(string name, string orderby)
        {
            var query = from project in _context.Set<Project>()
                        where project.Name == name
                        select project;
            switch (orderby.ToLower())
            {
                case "time":
                    return query.OrderBy(q => q.StartTime).ToList();
                case "name":
                    return query.OrderBy(q => q.Name).ToList();
                case "participants_number":
                    return query.OrderBy(q => q.ParticipantsNumber).ToList();
                default:
                    return query.ToList();
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            var project = _context.Project.Find(id);

            if (project == null)
            {
                return NotFound();
            }
            return project;
        }

        [HttpGet,Route("random")]
        public ActionResult<IEnumerable<Project>> GetRandomProjects()
        {
            var query = from project in _context.Set<Project>()
                        select project;
            var queryList = query.ToList();
            var rand = new Random();
            int len = queryList.Count;
            List<Project> result = new List<Project>();
            if(len <= 15){
                result = queryList;
            }
            else{
                int rd;
                List<int> tag = new List<int>(len + 1);
                for(int i = 0; i < len + 1; i++){
                    tag.Add(0);
                }
                for(int i = 0; i < 15; ){
                    rd = rand.Next(1, len);
                    if(tag[rd] == 0) {
                        tag[rd] = 1;
                        result.Add(queryList[rd]);
                        i++;
                    }
                    else{
                        rd = rand.Next(1, len);
                    }
                }
            }
            return result;
        }
        // PUT: api/Projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Project> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public ActionResult<Project> PostProject(Project project)
        {
            _context.Project.Add(project);
            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Project> DeleteProject(int id)
        {
            var project = _context.Project.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            _context.SaveChanges();

            return project;
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
