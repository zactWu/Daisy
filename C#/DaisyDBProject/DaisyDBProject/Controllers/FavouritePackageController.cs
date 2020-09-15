using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;

namespace DaisyDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritePackageController : ControllerBase
    {
        private readonly DaisyContext _context;

        public FavouritePackageController(DaisyContext context){
            _context = context;
        }

        // GET: api/FavouritePackage
        [HttpGet("{account}")]
        public ActionResult<IEnumerable<Object>> GetFavouritePackage(string account){
            var query = from favpack in _context.Set<FavouritePackage>()
                        where favpack.Account == account
                        select new { favpack.Name, favpack.CreateTime, favpack.Type };
            return query.ToList();
        }


        // PUT: api/FavouritePackage
        [HttpPut]
        public IActionResult PutFavouritePackage(FavouritePackage editFavPackage) {

            if (editFavPackage.Privacy != "public" && editFavPackage.Privacy != "private")
                return BadRequest();
            FavouritePackage fav =  _context.FavouritePackage.Find(editFavPackage.Account, editFavPackage.Name);
            if(fav == null) return NotFound();

            fav.Privacy = editFavPackage.Privacy;

            try{
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException){
                throw;
            }

            return NoContent();
        }

        // POST: api/FavouritePackage
        [HttpPost]
        public ActionResult<FavouritePackage> PostFavouritePackage(FavouritePackage favouritePackage){
            if (favouritePackage.Type != "moment" && favouritePackage.Type != "post")
                return BadRequest();

            if (favouritePackage.Privacy != "public" && favouritePackage.Privacy != "private")
                return BadRequest();

            _context.FavouritePackage.Add(favouritePackage);
            try{
                _context.SaveChanges();
            }
            catch (DbUpdateException){
                if (FavouritePackageExists(favouritePackage.Account, favouritePackage.Name)){
                    return Conflict();
                }
                else{
                    throw;
                }
            }

            return CreatedAtAction("GetFavouritePackage", new { id = favouritePackage.Account }, favouritePackage);
        }


        private bool FavouritePackageExists(string account, string name){
            return _context.FavouritePackage.Any(e => (e.Account == account && e.Name == name));
        }
    }
}
