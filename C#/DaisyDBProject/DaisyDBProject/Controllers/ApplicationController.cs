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

        

        // GET: /api/Application?ProjectId=[]&GroupId=[]
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetApplication(int ProjectId, int GroupId)
        {
            var result =
                from application in _context.Set<Application>()
                where application.ProjectId == ProjectId && application.GroupId == GroupId
                select new
                {
                    application.Account,
                    application.Content
                };

            return result.ToList();
        }

        // PUT: api/Application
        [HttpPut]
        public IActionResult PutApplication(int projectid, int groupid, string account, string result, Application application)
        {
            if (result == "successful")
            {
                var group = _context.Usergroups.Find(groupid, projectid);
                var num = group.Member.Count();
                num++;
                Member new_member = new Member {
                    ProjectId=projectid,
                    GroupId=groupid,
                    Account=account
                };
                //var new_member = _context.Users.Find(account);
                _context.Member.Add(new_member);
                application.Status = "successful";
            }
            else
            {
                application.Status = "failed";
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(projectid, groupid, account))
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
            application.Status = "Unprocessed";
            _context.Application.Add(application);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationExists(application.ProjectId, application.GroupId, application.Account))
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

        private bool ApplicationExists(int projectid, int groupid, string account)
        {
            return _context.Application.Any(e => e.ProjectId == projectid && e.GroupId == groupid && e.Account == account);
        }
    }
}
