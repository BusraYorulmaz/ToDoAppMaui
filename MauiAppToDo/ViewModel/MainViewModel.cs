using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppToDo.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    String to_do;

    [ObservableProperty]
    ObservableCollection<String> todoItems;

    [ObservableProperty]
    ObservableCollection<String> complatedList;
    

    public MainViewModel()
    {
        TodoItems = new ObservableCollection<String>();
        ComplatedList= new ObservableCollection<String>();
    }
 

    [RelayCommand]
    void AddToDo()
    {
        if (string.IsNullOrWhiteSpace(To_do))
            return;
        TodoItems.Add(To_do);
        To_do = string.Empty;
    }

    [RelayCommand]
    void Delete(string del)
    {
        if(TodoItems.Contains(del))
        {
            TodoItems.Remove(del);
        }
    }

    /*
    private bool isChecked;
    public bool IsChecked
    {
        get => isChecked;
        set => SetProperty(ref isChecked, value);
    }
    */

    [RelayCommand]
    void Complated(string completedTodo)
    {
       
            ComplatedList.Add(completedTodo);
            TodoItems.Remove(completedTodo);
       
    }


 

}