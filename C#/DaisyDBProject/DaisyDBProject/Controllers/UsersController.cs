using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDBProject.Models;
using DaisyDBProject;

namespace DaisyDBProject.Controllers {

    public class UserPut {
        public string icon { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string phoneNum { get; set; }
        public string emailAddress { get; set; }
        public string sex { get; set; }
        public string school { get; set; }
        public string college { get; set; }
        public int grade { get; set; }
        public string studentNumber { get; set; }
        public string intro { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly DaisyContext _context;

        public UsersController(DaisyContext context) {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetUsers(string name) {
            var query = from user in _context.Set<Users>()
                        where user.Name == name
                        select new {
                            icon = Helper.GetImageFromPath(user.Icon),
                            user.Account, user.Nickname
                        };            
            return query.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{account}")]
        public ActionResult<Users> GetUser(string account) {
            var users = _context.Users.Find(account);

            if (users == null) {
                return NotFound();
            }
            users.Password = "";
            users.Icon = Helper.GetImageFromPath(users.Icon);
            return users;
        }

        // PUT: api/Users/5
        [HttpPut("{account}")]
        public IActionResult PutUsers(string account, UserPut userput) {

            Users user = _context.Users.Find(account);
            user.Icon = Helper.PutImageIntoPath(userput.icon);
            user.Name = userput.name;
            user.Nickname = userput.nickname;
            user.PhoneNum = userput.phoneNum;
            user.EmailAddress = userput.emailAddress;
            user.Sex = userput.sex;
            user.School = userput.school;
            user.College = userput.college;
            user.Grade = userput.grade;
            user.StudentNumber = userput.studentNumber;
            user.Intro = userput.intro;

            try {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) {
                if (!UsersExists(account)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<Users> PostUsers(Users users) {
            users.Icon = Helper.PutImageIntoPath(users.Icon);
            _context.Users.Add(users);
            try {
                _context.SaveChanges();
            }
            catch (DbUpdateException) {
                if (UsersExists(users.Account)) {
                    return Conflict();
                }
                else {
                    throw;
                }
            }

            return CreatedAtAction("GetUsers", new { id = users.Account }, users);
        }

        private bool UsersExists(string id) {
            return _context.Users.Any(e => e.Account == id);
        }
    }
}
