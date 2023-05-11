using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApiDbContext _ApiDbContext;

        public UsersController(ApiDbContext apiDbContext)
        {
            _ApiDbContext = apiDbContext;
        }


        //All Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await (from user in _ApiDbContext.Users
                               select new
                               {
                                   Id = user.Id,
                                   UserName = user.UserName,
                                   UserEmail = user.UserEmail,
                                   UserPassword = user.UserPassword,
                               }).ToListAsync();
            return Ok(users);
        }

        ///  
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] Users user)
        {
            var userExists = _ApiDbContext.Users.FirstOrDefault(u => u.UserEmail == user.UserEmail);
            if (userExists != null)
            {
                return BadRequest("User with same email already exists");
            }
            _ApiDbContext.Users.Add(user);
            _ApiDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("[action]")]
        public  IActionResult UserLogin([FromQuery] string UserName, string UserPassword)
        {
            var result = _ApiDbContext.Users.FirstOrDefault(x=>x.UserName== UserName && x.UserPassword==UserPassword);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new List<Users>{ result});
           
        }
        

    }
}
