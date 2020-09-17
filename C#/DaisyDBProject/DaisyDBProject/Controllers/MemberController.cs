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
    public class MemberController : ControllerBase
    {
        private readonly DaisyContext _context;

        public MemberController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Member
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Member/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Member
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Member.Add(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberExists(member.ProjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMember", new { id = member.ProjectId }, member);
        }

        // DELETE: api/Member
        [HttpDelete]
        public ActionResult<Member> DeleteMember(int projectid, int groupid, string account)
        {
            var member = _context.Member.Find(projectid, groupid);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            _context.SaveChanges();

            return member;
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.ProjectId == id);
        }
    }
}
