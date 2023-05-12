using MauiAppToDo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

        //ekleme
        public async Task<ToDoLists> ToDoLists(ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/AddToDoList?userId={toDoLists.UserId}", toDoLists);
            response.EnsureSuccessStatusCode();
            var createdToDoList = await response.Content.ReadFromJsonAsync<ToDoLists>();
            return createdToDoList;
        }

        //belirli bir kullanıcıya ait tüm todo listelerini getirmek için
        public async Task<List<ToDoLists>> GetToDoLists(int userId)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/ToDos/GetToDoLists?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ToDoLists>>(content);
            }
            return null;
        }

        ////delete
        //public async Task<bool> DeleteToDoList(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"{ApiUrl}/ToDos/DeleteToDoList?id={id}");
        //    return response.IsSuccessStatusCode;
        //}

        //isActive
        public async Task<ToDoLists> SetInactive(ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/PutInActive?Id={toDoLists.Id}", toDoLists);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoLists>();
        }

        //complated
        public async Task<ToDoLists> ToDoComplated(ToDoLists toDoLists)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/ToDos/CompleteToDoList?id={toDoLists.Id}", toDoLists);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ToDoLists>();
        }
    }
}
