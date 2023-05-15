using CommunityToolkit.Maui.Views;
using MauiAppToDo.ViewModel;
using MauiToolkitPopupSample;
using System.ComponentModel;

namespace MauiAppToDo;

public partial class ToDoPage : ContentPage
{
    public ToDoPage(MainViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
        viewModel.InitializeAsync();

        NavigationPage.SetHasBackButton(this, false);
        //preferences de kaydolan veriler çaðýrýlýr 
        string userName = Preferences.Get("UserName", string.Empty);
        //string userEmail = Preferences.Get("UserEmail", string.Empty);

        LblUserName.Text = userName;
        //LblUserEmail.Text = userEmail;
    }

    private void BtnClickedPopup(object sender, EventArgs e)
    {
        this.ShowPopup(new PopupPageAddToDo());
    }


}