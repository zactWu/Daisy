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

namespace DaisyDBProject.Controllers {
    public class GroupPost {
        public int projectId { get; set; }
        public string leaderAccount { get; set; }
        public string postTime { get; set; }
        public string content { get; set; }
        public int maxMenberNum { get; set; }
        public string name { get; set; }

    }


    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase {
        private readonly DaisyContext _context;

        public PostController(DaisyContext context) {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetPost(int projectId) {
            var query = from post in _context.Set<Post>()
                        join usergroup in _context.Set<Usergroups>()
                            on new { post.GroupId, post.ProjectId } equals
                               new { usergroup.GroupId, usergroup.ProjectId } into groupPost
                        from gp in groupPost
                        join user in _context.Set<Users>()
                            on gp.LeaderAccount equals user.Account
                        select new {
                            post.PostId, post.GroupId, post.PostTime,
                            gp.LeaderAccount, user.Nickname, icon = ALiYunOss.GetImageFromPath(user.Icon)
                        };  

            return query.ToList();
        }

        // GET: api/Post/5
        [HttpGet("{postId}")]
        public ActionResult<Object> GetPost(int postId, int projectId, int groupId) {
            var post = _context.Post.Find(postId, projectId, groupId);

            var usergroup = _context.Usergroups.Find(groupId, projectId);

            var user = _context.Users.Find(usergroup.LeaderAccount);

            if (post == null || usergroup == null || user == null) {
                return NotFound();
            }

            return new {post.PostTime, post.Content, 
                usergroup.LeaderAccount, user.Nickname, icon = ALiYunOss.GetImageFromPath(user.Icon)
            };
        }

        // POST: api/Post
        [HttpPost]
        [Authorize]
        public ActionResult<Post> PostPost(GroupPost groupPost) {

            Usergroups usergroups = new Usergroups
                (groupPost.projectId, groupPost.leaderAccount, groupPost.name, groupPost.content);
            _context.Usergroups.Add(usergroups);
            _context.SaveChanges();

            Post post = new Post
                (groupPost.projectId, usergroups.GroupId, groupPost.postTime, groupPost.content, groupPost.maxMenberNum);
            _context.Post.Add(post);
            _context.SaveChanges();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }


        private bool PostExists(int id) {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
