using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using DaisyDBProject;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly DaisyContext _context;

        public CommentController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Comment?MomentId=
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetComment(string momentId){
            var query = from comment in _context.Set<Comment>()
                        join user in _context.Set<Users>()
                            on comment.Account equals user.Account
                        select new {
                            comment.CommentId, comment.Account,
                            comment.Content, comment.Time, user.Nickname,
                            Icon = Helper.GetImageFromPath(user.IconUrl)
                        };
            return query.ToList();
  
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public ActionResult<Object> GetComment(int id){
            var comment = _context.Comment.Find(id);
            if (comment == null){
                return NotFound();
            }
            var user = _context.Users.Find(comment.Account);
            var result = new {
                Icon = Helper.GetImageFromPath(user.IconUrl), user.Account,
                user.Nickname, comment.Content, comment.Time,
                ReplyList = (
                from reply in _context.Set<Reply>()
                where reply.CommentId == comment.CommentId
                select new { reply.ReplyId, reply.Account, reply.Time, reply.Content }
                ).ToList()             
            };
            return result;
        }

        // POST: api/Comment
        [HttpPost]
        public ActionResult<Comment> PostComment(Comment comment){
            _context.Comment.Add(comment);
            try {
                _context.SaveChanges();
            }
            catch (DbUpdateException) {
                throw;
            }

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public ActionResult<Comment> DeleteComment(int id){
            var comment =  _context.Comment.Find(id);
            if (comment == null){
                return NotFound();
            }

            _context.Comment.Remove(comment);
            _context.SaveChanges();

            return comment;
        }

    }
}
