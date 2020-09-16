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

       

        private bool SubscribeExists(int id) {
            return _context.Subscribe.Any(e => e.ProjectId == id);
        }
    }
}
