using MauiAppToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppToDo.Services.Abstract
{
    public interface IToDoListService
    {
        Task<ToDoLists> ToDoLists(ToDoLists toDoLists);
        Task<List<ToDoLists>> GetToDoLists(int userId);
        Task<ToDoLists> SetInactive(ToDoLists toDoLists);
        Task<ToDoLists> ToDoComplated(ToDoLists toDoLists);
    }
}
