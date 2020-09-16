using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly DaisyContext _context;

        public ApplicationController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplication()
        {
            return await _context.Application.ToListAsync();
        }

        // GET: /api/Application?ProjectId=[]&GroupId=[]
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetApplication(int ProjectId, int GroupId)
        {
            var project = _context.Application.Find(ProjectId);

            var group = _context.Application.Find(GroupId);

            if (project == null || group == null)
            {
                return NotFound();
            }

            var result =
                from application in _context.Set<Application>()
                where application.ProjectId == ProjectId && application.GroupId == GroupId
                select new
                {
                    application.Account,
                    application.Content,
                };

            return result.ToList();
        }

        // PUT: api/Application/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/Application
        [HttpPost]
        public ActionResult<Application> PostApplication(Application application)
        {
            _context.Application.Add(application);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationExists(application.ProjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplication", new { id = application.ProjectId }, application);
        }

        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.ProjectId == id);
        }
    }
}
