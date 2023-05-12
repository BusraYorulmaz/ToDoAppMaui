using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //Add
        [HttpPost("[action]")]
        public async Task<IActionResult> AddToDoList([FromBody] ToDoLists toDoLists)
        {
            // toDoLists.UserId = userId;
            _ApiDbContext.ToDoLists.Add(toDoLists);
            await _ApiDbContext.SaveChangesAsync();
            return Ok(toDoLists);
        }

        //list by userId
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoLists(int userId)
        {
            var toDoLists = await _ApiDbContext.ToDoLists.Where(x => x.UserId == userId && x.IsActive == true).ToListAsync();
            return Ok(toDoLists);
        }

        //list by userId
        [HttpGet("[action]")]
        public async Task<IActionResult> IsComplateList(int userId, bool isComplate)
        {
            var toDoLists = await _ApiDbContext.ToDoLists
                .Where(x => x.UserId == userId && x.IsComplete == isComplate && x.IsActive == true)
                .ToListAsync();
            return Ok(toDoLists);
        }

        ////delete
        //[HttpDelete("[action]")]
        //public IActionResult DeleteToDoList(int id)
        //{
        //    var toDoList = _ApiDbContext.ToDoLists.FirstOrDefault(x => x.Id == id);
        //    if (toDoList == null)  return NotFound();
        //    _ApiDbContext.ToDoLists.Remove(toDoList);
        //    _ApiDbContext.SaveChangesAsync();
        //    return Ok(toDoList);
        //}

        //isActive 
        [HttpPost("[action]")]
        public async Task<IActionResult> PutInActive(int Id)
        {
            var toDoLists = await _ApiDbContext.ToDoLists.FindAsync(Id);
            if (toDoLists is null) return NotFound();
            toDoLists.IsActive = false;
            _ApiDbContext.Entry(toDoLists).State = EntityState.Modified;
            await _ApiDbContext.SaveChangesAsync();
            return Ok(toDoLists);
        }

        //Complated
        [HttpPost("[action]")]
        public async Task<IActionResult> CompleteToDoList(int id)
        {
            var toDoList = await _ApiDbContext.ToDoLists.FindAsync(id);
            if (toDoList == null) return NotFound();
            toDoList.IsComplete = true;
            _ApiDbContext.Entry(toDoList).State = EntityState.Modified;
            await _ApiDbContext.SaveChangesAsync();
            return Ok(toDoList);
        }
    }
}
