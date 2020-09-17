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
    public class LikeMomentController : ControllerBase
    {
        private readonly DaisyContext _context;

        public LikeMomentController(DaisyContext context)
        {
            _context = context;
        }

        // POST: api/LikeMoments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LikeMoment>> PostLikeMoment(LikeMoment likeMoment)
        {
            _context.LikeMoment.Add(likeMoment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LikeMomentExists(likeMoment.MomentId,likeMoment.Account))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLikeMoment", new { id = likeMoment.MomentId }, likeMoment);
        }

        private bool LikeMomentExists(int id,string account)
        {
            return _context.LikeMoment.Any(e => e.MomentId == id && e.Account==account);
        }
    }
}
