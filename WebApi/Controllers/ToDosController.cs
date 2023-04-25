using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private ApiDbContext _ApiDbContext;

        public ToDosController(ApiDbContext apiDbContext)
        {
            _ApiDbContext = apiDbContext;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToDoList(int userId , [FromBody] ToDoLists toDoLists)
        {
            //kullanıcı var mı kontrol ediyoruz
           /* var user = await _ApiDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }*/

            // yeni bir todo ekliyoruz
            toDoLists.UserId= userId;
            _ApiDbContext.ToDoLists.Add(toDoLists);
            await _ApiDbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created); 

        }
    }
}
