using CommunityToolkit.Maui.Views;
using MauiAppToDo.ViewModel;
using MauiToolkitPopupSample;
using System.ComponentModel;

namespace MauiAppToDo;

public partial class ToDoPage : ContentPage
{
    public ToDoPage(MainViewModel mvm)
	{
		InitializeComponent();
        BindingContext= mvm;

        string  userName = Preferences.Get("UserName",string.Empty);
        string userEmail = Preferences.Get("UserEmail", string.Empty);
           
        LblUserName.Text = userName; //ismi bu sayfaya taþýrken
        LblUserEmail.Text = userEmail;
    }

     private void BtnClickedPopup(object sender, EventArgs e)
    {
        this.ShowPopup(new PopupPageAddToDo());
    }

   
}