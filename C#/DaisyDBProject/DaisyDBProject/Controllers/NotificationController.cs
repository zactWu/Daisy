using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using Microsoft.AspNetCore.Authorization;
using Project = DaisyDBProject.Models.Project;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly DaisyContext _context;

        public NotificationController(DaisyContext context)
        {
            _context = context;
        }


        [HttpGet("{account}")]
        public ActionResult<IEnumerable<Object>> GetNotification(string account)
        {
            var query = from subscribe in _context.Set<Subscribe>()
                        join project in _context.Set<Project>()
                        on subscribe.ProjectId equals project.ProjectId
                        into sub_proj
                        from sp in sub_proj
                        join notifi in _context.Set<Notification>()
                        on sp.ProjectId equals notifi.ProjectId
                        where subscribe.Account == account
                        select new { sp.Name, notifi.Title, notifi.Content, notifi.Time};

            return query.ToList();
        }

        [HttpPost]
        public IActionResult PostNotification(Notification notification)
        {
            _context.Notification.Add(notification);
            _context.SaveChanges();

            return Ok();
        }
    }
}
