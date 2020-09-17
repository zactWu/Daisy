using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using System.Threading.Tasks.Dataflow;
using DaisyDBProject.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly DaisyContext _context;

        public ReplyController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Reply
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetReply(int commentId)
        {
            var query = from reply in _context.Set<Reply>()
                        join user in _context.Set<Users>()
                            on reply.Account equals user.Account
                        where reply.CommentId == commentId
                        select new {
                            reply.Account, reply.ReplyId,
                            reply.Time, reply.Content, user.Nickname,
                            Icon = ALiYunOss.GetImageFromPath(user.Icon)
                        };
            return query.ToList();
        }


        // POST: api/Reply
        [HttpPost]
        [Authorize]
        public ActionResult<Reply> PostReply(Reply reply){
            _context.Reply.Add(reply);
            try {
                _context.SaveChanges();
            }
            catch (DbUpdateException) {
                throw;
            }

            return CreatedAtAction("GetReply", new { id = reply.ReplyId }, reply);
        }

        // DELETE: api/Reply/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Reply> DeleteReply(int id){
            var reply = _context.Reply.Find(id);
            if (reply == null){
                return NotFound();
            }
            _context.Reply.Remove(reply);
            _context.SaveChanges();

            return reply;
        }
    }
}
