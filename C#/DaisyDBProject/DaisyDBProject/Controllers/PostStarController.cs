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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostStarController : ControllerBase
    {
        private readonly DaisyContext _context;

        public PostStarController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/PostStar
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetPostStar(string account, string name){
            var query = from postStar in _context.Set<PostStar>()
                        join post in _context.Set<Post>()
                            on new { postStar.ProjectId, postStar.GroupId, postStar.PostId }
                            equals new { post.ProjectId, post.GroupId, post.PostId }
                        where postStar.Account == account && postStar.Name == name
                        select new { post.ProjectId, post.GroupId, post.PostId, post.Content };
            return query.ToList();
        }


        // POST: api/PostStar
        [HttpPost]
        public ActionResult<PostStar> PostPostStar(PostStar postStar){
            _context.PostStar.Add(postStar);
            try { 
                 _context.SaveChanges();
            }
            catch (DbUpdateException){
                if (PostStarExists(postStar)){
                    return Conflict();
                }
                else{
                    throw;
                }
            }

            return CreatedAtAction("GetPostStar", new { id = postStar.ProjectId }, postStar);
        }

        // DELETE: api/PostStar/5
        [HttpDelete]
        public ActionResult<PostStar> DeletePostStar
            (int projectId, int groupId, int postId, string account, string name){
            var postStar = _context.PostStar.Find(projectId, groupId, account, postId, name); ;
            if (postStar == null){
                return NotFound();
            }

            _context.PostStar.Remove(postStar);
            _context.SaveChanges();

            return postStar;
        }

        private bool PostStarExists(PostStar poststar)
        {
            return _context.PostStar.Any(e => (e.ProjectId == poststar.ProjectId &&
            e.GroupId == poststar.GroupId && e.PostId == poststar.PostId
            && e.Account == poststar.Account && e.Name == poststar.Name));
        }
    }
}
