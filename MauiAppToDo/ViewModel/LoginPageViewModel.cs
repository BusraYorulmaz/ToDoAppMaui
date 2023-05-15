using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppToDo.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppToDo.ViewModel
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly LoginService _loginService = new LoginService();

        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string userPassword;

        [RelayCommand]
        private async Task Login()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserPassword))
            {
                var users = await _loginService.Login(UserName, UserPassword);
                if (users != null)
                {
                    Preferences.Set("UserId", users.Id);
                    Preferences.Set("UserName", users.UserName);
                    Preferences.Set("UserEmail", users.UserEmail);
                    Preferences.Set("UserPassword", users.UserPassword);
                    await Application.Current.MainPage.Navigation.PushAsync(new ToDoPage(new MainViewModel()));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Please Enter Correct Username And Password", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please Input Username and password", "Ok");
            }
        }






    }
}
