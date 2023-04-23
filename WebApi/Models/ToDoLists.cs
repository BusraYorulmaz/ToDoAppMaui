namespace WebApi.Models
{
    public class ToDoLists
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IsComplete { get; set; } //1,0
    }
}
