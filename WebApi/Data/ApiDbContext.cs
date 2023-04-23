using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApiDbContext :DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<ToDoLists> ToDoLists { get; set; }
    }
}
