using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace DaisyDBProject.Controllers
{
    public class MomentItem{
        public Moment moment{get; set;}
        public string icon{get; set;}
        public string nickname{get; set;}
        public int likeCount{get; set;}
        public int commentCount{get; set;}
        public int starCount{get; set;}
    }


    [Route("api/[controller]")]
    [ApiController]
    public class MomentController : ControllerBase
    {
        private readonly DaisyContext _context;

        public MomentController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/Moments
        [HttpGet]
        public ActionResult<IEnumerable<Moment>> GetMoment()
        {
            return _context.Moment.ToList();
        }

        [HttpGet,Route("count")]
        public int GetMomentCount()
        {
            return _context.Moment.Count();
        }

        // GET: api/Moments/5
        [HttpGet("{id}")]
        public ActionResult<Moment> GetMoment(int id)
        {
            var moment = _context.Moment.Find(id);

            if (moment == null)
            {
                return NotFound();
            }

            return moment;
        }

        [HttpGet,Route("search")]
        public ActionResult<IEnumerable<MomentItem>> SearchMoment(string name, string orderby)
        {
            var query = from mom in _context.Set<Moment>()
                        from user in _context.Set<Users>()
                        where mom.Account == user.Account && mom.Title == name
                        select new MomentItem {
                            moment = mom, icon = user.Icon, nickname = user.Nickname,
                            likeCount = (from like in _context.Set<LikeMoment>()
                                         where like.Account == mom.Account
                                         select like.Account).Count(),
                            commentCount = (from comment in _context.Set<Comment>()
                                            where comment.Account == mom.Account
                                            select comment.Account).Count(),
                            starCount = (from star in _context.Set<MomentStar>()
                                         where star.Account == mom.Account
                                         select star.Account).Count()
                        };
            switch (orderby.ToLower())
            {
                case "time":
                    return query.OrderBy(q => q.moment.Time).ToList();
                case "name":
                    return query.OrderBy(q => q.moment.Title).ToList();
                default:
                    return query.ToList();
            }
        }

        [HttpGet, Route("random")]
        public ActionResult<IEnumerable<MomentItem>> GetRandomMoment()
        {
            var query = from mom in _context.Set<Moment>()
                        from user in _context.Set<Users>()
                        where mom.Account == user.Account 
                        select new MomentItem{moment = mom, icon = user.Icon, nickname = user.Nickname, 
                            likeCount = (from like in _context.Set<LikeMoment>()
                                             where like.Account == mom.Account
                                             select like.Account).Count(), 
                            commentCount = (from comment in _context.Set<Comment>()
                                            where comment.Account == mom.Account
                                            select comment.Account).Count(), 
                            starCount = (from star in _context.Set<MomentStar>()
                                         where star.Account == mom.Account
                                         select star.Account).Count()
                        };
            var result = query.OrderBy(q => Guid.NewGuid()).Take(5);
            return result.ToList();
        }


        // POST: api/Moments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public ActionResult<Moment> PostMoment(Moment moment)
        {
            _context.Moment.Add(moment);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetMoment", new { id = moment.MomentId }, moment);
        }

        // DELETE: api/Moments/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Moment> DeleteMoment(int id)
        {
            var moment = _context.Moment.Find(id);
            if (moment == null)
            {
                return NotFound();
            }

            _context.Moment.Remove(moment);
            _context.SaveChanges();

            return moment;
        }

        private bool MomentExists(int id)
        {
            return _context.Moment.Any(e => e.MomentId == id);
        }
    }
}
