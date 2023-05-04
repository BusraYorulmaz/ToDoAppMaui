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
      //  private const string ApiUrl = "https://192.168.64.1:45457/api";
        
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
            var response = await _httpClient.GetAsync($"{ApiUrl}/ToDos/GetToDoLists?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ToDoLists>>(content);
            }
            return null;
        }

        //https://localhost:7072/api/ToDos/DeleteToDoList?id=60
        public async Task<bool> DeleteToDoList(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/ToDos/DeleteToDoList?id={id}");
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        //public async Task DeleteToDoList(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"{ApiUrl}/ToDos/DeleteToDoList?id={id}");
        //    response.EnsureSuccessStatusCode();
        //}

        //// bu kısım hatalı ????
        //////https://192.168.64.1:45457/api/ToDos/CompleteToDoList?id=59
        ////update 
        //public async Task<ToDoLists> CompleteToDoList(int id)
        //{
        //    var response = await _httpClient.PostAsync($"{ApiUrl}/ToDos/CompleteToDoList?id={id}", null);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var updateToDoList = JsonConvert.DeserializeObject<ToDoLists>(content);
        //        return updateToDoList;
        //    }
        //    return null;

        //}

    }
}
