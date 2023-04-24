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

        //preferences den adý yükle
        // userName = Preferences.Get("UserName", "");
        LblUserName.Text = userName; //ismi bu sayfaya taþýrken
    }

     private void BtnClickedPopup(object sender, EventArgs e)
    {
    

        this.ShowPopup(new PopupPageAddToDo());
    }

   
}