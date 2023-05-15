using MauiAppToDo.Models;
using MauiAppToDo.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiAppToDo.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private const string ApiUrl = "https://192.168.64.1:45455/api";

        private readonly HttpClient _httpClient;

        public LoginService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);

        }

        public async Task<Users> Login(string username, string password)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/Users/UserLogin?UserName={username}&UserPassword={password}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Users>>(jsonString);
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
