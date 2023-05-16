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
            try
            {
                _ApiDbContext.ToDoLists.Add(toDoLists);
                await _ApiDbContext.SaveChangesAsync();
                return Ok(toDoLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }

        //list by userId
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToDoLists(int userId)
        {
            try
            {
                var toDoLists = await _ApiDbContext.ToDoLists.Where(x => x.UserId == userId && x.IsActive == true).ToListAsync();
                return Ok(toDoLists);
            }
            catch (Exception ex)
            {
                return StatusCode(404, "Bir hata oluştu: " + ex.Message);
            }
        }


        //isActive 
        [HttpPost("[action]")]
        public async Task<IActionResult> PutInActive(int Id)
        {
            try
            {
                var toDoLists = await _ApiDbContext.ToDoLists.FindAsync(Id);
                if (toDoLists is null) return NotFound();
                toDoLists.IsActive = false;
                _ApiDbContext.Entry(toDoLists).State = EntityState.Modified;
                await _ApiDbContext.SaveChangesAsync();
                return Ok(toDoLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }

        //Complated
        [HttpPost("[action]")]
        public async Task<IActionResult> CompleteToDoList(int id)
        {
            try
            {
                var toDoList = await _ApiDbContext.ToDoLists.FindAsync(id);
                if (toDoList == null) return NotFound();
                toDoList.IsComplete = true;
                _ApiDbContext.Entry(toDoList).State = EntityState.Modified;
                await _ApiDbContext.SaveChangesAsync();
                return Ok(toDoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }

        //Update
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateToDoList(ToDoLists toDoLists)
        {
            try
            {
                var updateToDo = await _ApiDbContext.ToDoLists.FindAsync(toDoLists.Id);
                if (updateToDo == null) return NotFound("ToDo Bulunmadı");
                
                updateToDo.Title = toDoLists.Title;
                updateToDo.Description = toDoLists.Description;
                _ApiDbContext.Entry(updateToDo).State = EntityState.Modified;

                await _ApiDbContext.SaveChangesAsync();
                return Ok(updateToDo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }
    }
}
