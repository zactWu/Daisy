using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using DaisyDBProject.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsergroupController : ControllerBase
    {
        private readonly DaisyContext _context;

        public UsergroupController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Usergroup
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Usergroups>>> GetUsergroups()
        //{
        //    return await _context.Usergroups.ToListAsync();
        //}

        // GET: api/Usergroup/111111
        [HttpGet("{account}")]
        public ActionResult<IEnumerable<Object>> GetUsergroups(string account)
        {
            var usr = _context.Users.Find(account);
            if (usr == null)
            {
                return NotFound();
            }

            var result =
                from member in _context.Set<Member>()
                join usergroup in _context.Set<Usergroups>()
                on member.GroupId equals usergroup.GroupId
                where member.Account == account
                select new
                {
                    usergroup.GroupId,
                    usergroup.ProjectId,
                    usergroup.LeaderAccount,
                    usergroup.Name,
                    usergroup.Introduction,
                };
                
            return result.ToList();
        }

        // GET: api/Usergroup?GroupId=[]&ProjectId=[]
        [HttpGet]
        public ActionResult<Object> GetUsergroups(int groupid, int projectid)
        {
            var group = _context.Usergroups.Find(groupid, projectid);
            var leaderusr = _context.Users.Find(group.LeaderAccount);
            var result = new {
                Curmemnum = group.Member.Count(),
                Icon = Helper.GetImageFromPath(leaderusr.Icon),
                sequence = (
                from project in _context.Set<Project>()
                join post in _context.Set<Post>()
                on project.ProjectId equals post.ProjectId
                where project.ProjectId == projectid
                select new
                {
                    project.Name,
                    post.MaxMemberNum,
                    
                }).ToList()
            };
            return result;
            }

        // PUT: api/Usergroup
        [HttpPut]
        public IActionResult PutUsergroups(Usergroups usergroups)
        {

            Usergroups group = _context.Usergroups.Find(usergroups.GroupId,usergroups.ProjectId);
            group.Name = usergroups.Name;
            group.Introduction = usergroups.Introduction;

            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsergroupsExists(usergroups.GroupId))
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


        // DELETE: api/Usergroup/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usergroups>> DeleteUsergroups(int id)
        {
            var usergroups = await _context.Usergroups.FindAsync(id);
            if (usergroups == null)
            {
                return NotFound();
            }

            _context.Usergroups.Remove(usergroups);
            await _context.SaveChangesAsync();

            return usergroups;
        }

        private bool UsergroupsExists(int id)
        {
            return _context.Usergroups.Any(e => e.GroupId == id);
        }
    }
}
