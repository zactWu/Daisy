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
    public class MessageController : ControllerBase
    {
        private readonly DaisyContext _context;

        public MessageController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Message?Account=[]
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetMessage(string Account)
        {
            var user = _context.Users.Find(Account);
            if (user == null)
            {
                return NotFound();
            }

            var result =
                from msg in _context.Set<Message>()
                join usr in _context.Set<Users>()
                    on msg.ReceiveAccount equals user.Account
                where usr.Account == Account
                select new
                {
                    msg.MessageId,
                    msg.SendAccount,
                    msg.Content,
                    msg.Time,
                    usr.Nickname
                };

            return result.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Message> GetMessage(int id)
        {
            var message = _context.Message.Find(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // POST: api/Message
        [HttpPost]
        public ActionResult<Message> PostMessage(Message message)
        {
            _context.Message.Add(message);
            try{
                _context.SaveChanges();
            }
            catch(DbUpdateException) {
                throw;
            }

            return CreatedAtAction("GetMessage", new { id = message.MessageId }, message);
        }
    }
}
