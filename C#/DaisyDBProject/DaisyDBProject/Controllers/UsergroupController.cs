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

            var result1 =
                from member in _context.Set<Member>()
                from usergroup in _context.Set<Usergroups>()
                where member.GroupId == usergroup.GroupId && member.Account == account
                select new
                {
                    usergroup.GroupId,
                    usergroup.ProjectId,
                    usergroup.LeaderAccount,
                    usergroup.Name,
                    usergroup.Introduction,
                };
            var result2 = 
                from usergroup in _context.Set<Usergroups>()
                where usergroup.LeaderAccount == account
                select new
                {
                    usergroup.GroupId,
                    usergroup.ProjectId,
                    usergroup.LeaderAccount,
                    usergroup.Name,
                    usergroup.Introduction,
                };
                
            return result1.ToList().Union(result2.ToList()).ToList();
        }

        // GET: api/Usergroup?GroupId=[]&ProjectId=[]
        [HttpGet]
        public ActionResult<Object> GetUsergroups(int groupid, int projectId){
            var group = _context.Usergroups.Find(groupid, projectId);
            if(group == null) return BadRequest();
            var leaderusr = _context.Users.Find(group.LeaderAccount);
            var project = _context.Project.Find(projectId);
            var result = new{
                group.Name, group.Introduction, 
                memberList = 
               (from member in _context.Set<Member>()
                join user in _context.Set<Users>()
                    on member.Account equals user.Account
                where member.GroupId == groupid && member.ProjectId == projectId
                select new{user.Account, user.Name, 
                icon = ALiYunOss.GetImageFromPath(user.Icon)}
                ).ToList()
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
        public ActionResult<Usergroups> DeleteUsergroups(int id, int projectId)
        {
            var usergroups = _context.Usergroups.Find(id, projectId);
            if (usergroups == null)
            {
                return NotFound();
            }

            _context.Usergroups.Remove(usergroups);
            _context.SaveChanges();

            return usergroups;
        }

        private bool UsergroupsExists(int id)
        {
            return _context.Usergroups.Any(e => e.GroupId == id);
        }
    }
}
