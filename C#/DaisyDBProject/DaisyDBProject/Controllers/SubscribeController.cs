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
    public class SubscribeController : ControllerBase {
        private readonly DaisyContext _context;
        
        public SubscribeController(DaisyContext context) {
            _context = context;
        }

        // GET: api/Subscribe
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetSubscribe() {
            var query = from subscribe in _context.Set<Subscribe>()
                        join project in _context.Set<Project>()
                            on subscribe.ProjectId equals project.ProjectId
                        select new { project.ProjectId, project.Name };
            return query.ToList();
        }

        [HttpPost]
        public ActionResult<Subscribe> PostSubscribe(Subscribe subscribe)
        {
            _context.Subscribe.Add(subscribe);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SubscribeExists(subscribe))
                {
                    return Conflict();
                }
                else if (!UserExists(subscribe.Account))
                {
                    return BadRequest();
                }
                else if (!ProjectExists(subscribe.ProjectId))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubscribe", new { id = subscribe.ProjectId }, subscribe);
        }

        private bool SubscribeExists(Subscribe subscribe)
        {
            return _context.Subscribe.Any(e => (e.ProjectId == subscribe.ProjectId) &&
            (e.Account == subscribe.Account));
        }
        private bool UserExists(string account)
        {
            return _context.Users.Any(e => e.Account == account);
        }
        private bool ProjectExists(int projectId)
        {
            return _context.Project.Any(e => e.ProjectId == projectId);
        }


        //private bool SubscribeExists(int id) {
        //    return _context.Subscribe.Any(e => e.ProjectId == id);
        //}
    }
}
