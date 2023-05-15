using CommunityToolkit.Maui.Views;
using MauiAppToDo.Models;
using MauiAppToDo.Services.Concrete;
using MauiAppToDo.ViewModel;
using MauiToolkitPopupSample;
using System.ComponentModel;

namespace MauiAppToDo;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel();
    }  
}

