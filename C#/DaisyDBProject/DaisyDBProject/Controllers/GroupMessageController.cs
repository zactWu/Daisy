using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;

namespace DaisyDBProject.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMessageController : ControllerBase {
        private readonly DaisyContext _context;

        public GroupMessageController(DaisyContext context) {
            _context = context;
        }

        // GET: api/GroupMessage
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetGroupMessage(string account) {
            Users user = _context.Users.Find(account);
            var query = from userGroupMessage in _context.Set<UserGroupMessage>()
                        join groupMessage in _context.Set<GroupMessage>()
                            on userGroupMessage.GroupMessageId equals groupMessage.GroupMessageId into UsrGpMs
                        from usrGpMs in UsrGpMs
                        join userGroup in _context.Set<Usergroups>()
                            on usrGpMs.GroupId equals userGroup.GroupId into UGMG
                        from ugmg in UGMG
                        join project in _context.Set<Project>()
                            on ugmg.ProjectId equals project.ProjectId
                        where userGroupMessage.Account == account
                        select new {
                            usrGpMs.GroupMessageId, usrGpMs.GroupId, usrGpMs.ProjectId, usrGpMs.Time, usrGpMs.Content,
                            GroupName = ugmg.Name, ProjectName = project.Name
                        };
            return query.ToList();
        }

    }
}
