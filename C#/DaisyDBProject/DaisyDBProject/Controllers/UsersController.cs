using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DaisyDBProject.Helpers;
using DaisyDBProject.Models;
using Microsoft.AspNetCore.Authorization;

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

    public class LoginUser {
        public string account { get; set; }
        public string password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly DaisyContext _context;
        private readonly TokenManagement _tokenManagement;

        public UsersController(DaisyContext context, IOptions<TokenManagement> tokenManagement) {
            _context = context;
            _tokenManagement = tokenManagement.Value;           
        }

        private bool IsAuthenticated(LoginUser request, out string token){
            token = string.Empty;
            var claims = new[]{
                new Claim(ClaimTypes.Name,request.account)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetUsers(string name) {
            var query = from user in _context.Set<Users>()
                        where user.Name == name
                        select new {
                            icon = ALiYunOss.GetImageFromPath(user.Icon),
                            user.Account, user.Nickname
                        };            
            return query.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{account}")]
        [Authorize]
        public ActionResult<Users> GetUser(string account) {
            var users = _context.Users.Find(account);

            if (users == null) {
                return NotFound();
            }
            users.Password = "";
            users.Icon = ALiYunOss.GetImageFromPath(users.Icon);
            return users;
        }

        // PUT: api/Users/5
        [HttpPut("{account}")]
        [Authorize]
        public IActionResult PutUsers(string account, UserPut userput) {

            ALiYunOss.PutImageIntoPath(userput.icon, account);
            Users user = _context.Users.Find(account);
            user.Icon = account;
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
            ALiYunOss.PutImageIntoPath(users.Icon, users.Account);
            users.Icon = users.Account;
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



        // POST: api/Users/Login
        [HttpPost]
        [Route("Login")]
        public ActionResult<Object> Login(LoginUser loginUser) {
            Users user = _context.Users.Find(loginUser.account);
            if(user == null)
                return BadRequest("Account doesn't exist1");
            if (user.Password != loginUser.password) 
                return BadRequest("Password wrong!");
            string token;
            if (IsAuthenticated(loginUser, out token)){
                return Ok(new {jwt = token});
            }
            return BadRequest("Invalid Request");
        }

        private bool UsersExists(string id) {
            return _context.Users.Any(e => e.Account == id);
        }
    }
}
