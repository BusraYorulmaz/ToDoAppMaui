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
        private const string ApiUrl = "https://localhost:7072/api";
        private readonly HttpClient _httpClient;

        public ToDoListService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ToDoLists> ToDoLists(int userId,ToDoLists toDoLists)
        {          
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/AddToDoList?userId={userId}",toDoLists);
            response.EnsureSuccessStatusCode();
            var createdToDoList = await response.Content.ReadFromJsonAsync<ToDoLists>(); 
            return createdToDoList;
        }

        //belirli bir kullanıcıya ait tüm todo listelerini getirmek için
        public async Task<List<ToDoLists>> GetToDoLists(int userId)
        {
            //https://localhost:44356/api/ToDos/GetToDoLists?userId={userId}
            var response = await _httpClient.GetAsync($"{ApiUrl}/ToDos/GetToDoLists?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ToDoLists>>(content);
            }
            return null;
        }

        public async Task<bool> DeleteToDoList(int id)
        {
            //https://localhost:7072/api/ToDos/DeleteToDoList?id=6
            var response = await _httpClient.PostAsync($"{ApiUrl}/ToDos/DeleteToDoList?id={id}", null);
            return response.IsSuccessStatusCode;
        }


    }
}
