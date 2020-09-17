using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DaisyDBProject.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DaisyDBProject.Controllers {
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserGetController : ControllerBase {

        private readonly DaisyContext _context;
        public UserGetController(DaisyContext context) {
            _context = context;
        }

        // GET: /api/User/Moment/111111111
        [HttpGet]
        [Route("Moment/{account}")]
        public ActionResult<IEnumerable<Object>> GetUsrMoment(string account) {
            var query = from moment in _context.Set<Moment>()
                        where moment.Account == account
                        select new { moment.MomentId, moment.Title };
            return query.ToList();
        }

        // GET: /api/User/Coment/111111111
        [HttpGet]
        [Route("Coment/{account}")]
        public ActionResult<IEnumerable<Object>> GetUsrComent(string account) {
            var query = from disc in _context.Set<Discussion>()
                        join project in _context.Set<Project>()
                            on disc.ProjectId equals project.ProjectId
                        where disc.Account == account
                        select new { disc.ProjectId, disc.DiscussionId, project.Name }; ;
            return query.ToList();
        }

        // GET: /api/User/Post/111111111
        [HttpGet]
        [Route("Post/{account}")]
        public ActionResult<IEnumerable<Object>> GetUsrPost(string account) {
            var query = from usergroup in _context.Set<Usergroups>()
                        join post in _context.Set<Post>()
                            on usergroup.GroupId equals post.GroupId
                        where usergroup.LeaderAccount == account
                        select new { usergroup.ProjectId, usergroup.GroupId, post.PostId, usergroup.Name };
            return query.ToList();
        }

        // GET: /api/User/Reply/111111111
        [HttpGet]
        [Route("Reply/{account}")]
        public ActionResult<IEnumerable<Object>> GetUsrReply(string account) {
            var query = from reply in _context.Set<Reply>()
                        join user in _context.Set<Users>()
                            on reply.Account equals user.Account
                        where reply.Account == account
                        select new { 
                            reply.ReplyId, reply.Account, user.Nickname, 
                            reply.Content, reply.Time, reply.CommentId 
                        };
            return query.ToList();
        }
    }
}
