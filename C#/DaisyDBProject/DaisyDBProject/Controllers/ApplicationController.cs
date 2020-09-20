using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace DaisyDBProject.Controllers
{

    public class ApplicationPut{
        public int projectId { get; set;}
        public string account { get; set;}
        public int groupId { get; set;}
        public string result { get; set;}

    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly DaisyContext _context;

        public ApplicationController(DaisyContext context)
        {
            _context = context;
        }

        

        // GET: /api/Application/1
        [HttpGet("{account}")]
        public ActionResult<IEnumerable<Object>> GetApplication(string account){
            var result =
                from application in _context.Set<Application>()
                from usergroup in _context.Set<Usergroups>()
                from user in _context.Set<Users>()
                where application.GroupId == usergroup.GroupId && usergroup.LeaderAccount == account
                select new{
                    usergroup.ProjectId, usergroup.GroupId, usergroup.Name, 
                    application.Account, userName = user.Name, application.Content, application.Status
                };

            return result.ToList();
        }

        // PUT: api/Application
        [HttpPut]
        public IActionResult PutApplication(ApplicationPut applicationPut)
        {
            int projectid = applicationPut.projectId;
            int groupid = applicationPut.groupId;
            var application = _context.Application.Find(projectid, groupid, applicationPut.account);
            if (applicationPut.result == "successful"){
                var group = _context.Usergroups.Find(groupid, projectid);
                Member new_member = new Member {
                    ProjectId = projectid,
                    GroupId = groupid,
                    Account = applicationPut.account
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
                if (!ApplicationExists(projectid, groupid, applicationPut.account))
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
        [Authorize]
        public IActionResult PostApplication(Application application)
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

            return Ok();
        }

        private bool ApplicationExists(int projectid, int groupid, string account)
        {
            return _context.Application.Any(e => e.ProjectId == projectid && e.GroupId == groupid && e.Account == account);
        }
    }
}
