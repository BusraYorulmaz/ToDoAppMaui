using CommunityToolkit.Maui.Views;
using MauiAppToDo.ViewModel;
using MauiToolkitPopupSample;
using System.ComponentModel;

namespace MauiAppToDo;

public partial class ToDoPage : ContentPage
{
    public ToDoPage(MainViewModel mvm, string userName)
	{
		InitializeComponent();
        BindingContext= mvm;

        //preferences den ad� y�kle
        // userName = Preferences.Get("UserName", "");
        LblUserName.Text = userName; //ismi bu sayfaya ta��rken
    }

     private void BtnClickedPopup(object sender, EventArgs e)
    {
    

        this.ShowPopup(new PopupPageAddToDo());
    }

   
}