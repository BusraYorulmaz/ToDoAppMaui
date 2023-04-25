using CommunityToolkit.Maui.Views;
using MauiAppToDo.Models;
using MauiAppToDo.Services;
using MauiAppToDo.ViewModel;
using MauiToolkitPopupSample;
using System.ComponentModel;

namespace MauiAppToDo;

public partial class MainPage : ContentPage
{
    private readonly LoginService _loginService = new LoginService();
   
    public MainPage()
	{
		InitializeComponent(); 
		//string name = Preferences.Get("UserName", "");//preferences den adı yükle
        // EnterUserName.Text = name;
    }

    private async void  BtnMoveToToDoPage_Clicked(object sender, EventArgs e)
	{
        string userName = EnterUserName.Text;
        string userPassword = EnterPassword.Text;

        if (userName!=null && userPassword!=null)
        {
            var users=await _loginService.Login(userName, userPassword);
            if (users != null) 
            {         
                
                //Bilgiler preferencese kaydedilir 
                Preferences.Set("UserId",users.Id.ToString());
                Preferences.Set("UserName", users.UserName);
                Preferences.Set("UserEmail", users.UserEmail);
                Preferences.Set("UserPassword", users.UserPassword);

               await Navigation.PushAsync(new ToDoPage(new MainViewModel()));

            }
            else
            {
                await DisplayAlert("Warning", "Please Enter Correct Username And Password", "Ok");
            }
           
        }
        else
        {
           await DisplayAlert("Warning", "Please Input Username and password", "Ok");
        }



    }
        


}

