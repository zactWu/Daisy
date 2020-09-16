using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using Microsoft.CodeAnalysis;
using Project = DaisyDBProject.Models.Project;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly DaisyContext _context;

        public DiscussionController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Discussions
        [HttpGet]
        public ActionResult<IEnumerable<Discussion>> GetDiscussion(int projectId)
        {
            var query = from project in _context.Set<Project>()
                        join discusson in _context.Set<Discussion>()
                        on project.ProjectId equals discusson.ProjectId
                        where project.ProjectId == projectId
                        select discusson;
            return query.ToList();
        }


        // PUT: api/Discussions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public IActionResult PutDiscussion(Discussion discussion)
        {
            _context.Entry(discussion).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscussionExists(discussion.DiscussionId,discussion.ProjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Discussions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Discussion> PostDiscussion(Discussion discussion)
        {
            _context.Discussion.Add(discussion);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetDiscussion", new { id = discussion.DiscussionId }, discussion);
        }

        // DELETE: api/Discussions/5
        public ActionResult<Discussion> DeleteDiscussion(int id,int projectId)
        {
            var discussion = _context.Discussion.Find(id, projectId);
            if (discussion == null)
            {
                return NotFound();
            }

            _context.Discussion.Remove(discussion);
            _context.SaveChanges();

            return discussion;
        }

        private bool DiscussionExists(int id, int projectId)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id &&
            e.ProjectId==projectId);
        }
    }
}
