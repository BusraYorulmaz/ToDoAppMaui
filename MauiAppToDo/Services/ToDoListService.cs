using MauiAppToDo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppToDo.Services
{
    public class ToDoListService
    {
        private readonly HttpClient _httpClient;

        public ToDoListService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ToDoLists> ToDoLists(int userId,ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:44356/api/ToDos/{userId}",toDoLists);

            response.EnsureSuccessStatusCode();
            var createdToDoList = await response.Content.ReadFromJsonAsync<ToDoLists>();
            return createdToDoList;
 
        }
    }
}
