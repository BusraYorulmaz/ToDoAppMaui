using MauiAppToDo.Models;
using MauiAppToDo.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MauiAppToDo.Services.Concrete
{
    public class ToDoListService : IToDoListService
    {
        private const string ApiUrl = "https://192.168.64.1:45455/api";

        private readonly HttpClient _httpClient;
        public ToDoListService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
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
