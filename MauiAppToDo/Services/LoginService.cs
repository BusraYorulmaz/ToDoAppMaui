﻿using MauiAppToDo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiAppToDo.Services
{
    public class LoginService {
        private const string ApiUrl = "https://localhost:7072/api/";
        //https://localhost:7072/api/Users/UserLogin?UserName=Busra&UserPassword=123456

        private readonly HttpClient _httpClient;

        public LoginService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Users> Login(string username, string password)
        { 
            var response = await _httpClient.GetAsync($"{ApiUrl}Users/UserLogin?UserName={username}&UserPassword={password}");

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
