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
    public class MomentStarController : ControllerBase
    {
        private readonly DaisyContext _context;

        public MomentStarController(DaisyContext context)
        {
            _context = context;
        }

        // GET: api/MomentStar
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetMomentStar(string account, string name){

            var query = from momentStar in _context.Set<MomentStar>()
                        join moment in _context.Set<Moment>()
                            on momentStar.MomentId equals moment.MomentId
                        where momentStar.Account == account && momentStar.Name == name
                        select new { moment.MomentId, moment.Title };
                        
            return query.ToList();
        }

        // POST: api/MomentStar
        [HttpPost]
        public ActionResult<MomentStar> PostMomentStar(MomentStar momentStar){
            _context.MomentStar.Add(momentStar);
            try{
                _context.SaveChanges();
            }
            catch (DbUpdateException){
                if (MomentStarExists(momentStar)){
                    return Conflict();
                }
                else if (!MomentExists(momentStar.MomentId)) {
                    return BadRequest();
                }
                else if (!FavouritePackageExists(momentStar.Account, momentStar.Name)) {
                    return BadRequest();
                }
                else {
                    throw;
                }
            }
            return CreatedAtAction("GetMomentStar", new { id = momentStar.MomentId }, momentStar);
        }

        // DELETE: api/MomentStar
        [HttpDelete()]
        public ActionResult<MomentStar> DeleteMomentStar(int momentId, string account, string name){
            var momentStar = _context.MomentStar.Find(momentId, account, name);
            if (momentStar == null){
                return NotFound();
            }

            _context.MomentStar.Remove(momentStar);
            _context.SaveChanges();

            return momentStar;
        }

        private bool MomentStarExists(MomentStar momentStar){
            return _context.MomentStar.Any(e => (e.MomentId == momentStar.MomentId) 
            && (e.Account == momentStar.Account) && (e.Name == momentStar.Name));
        }

        private bool MomentExists(int id) {
            return _context.Moment.Any(e => e.MomentId == id);
        }

        private bool FavouritePackageExists(string account, string name) {
            return _context.FavouritePackage.Any(e => (e.Account == account && e.Name == name));
        }
    }
}
