using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppToDo.Models;
using MauiAppToDo.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAppToDo.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly ToDoListService _toDoListService;
     

    [ObservableProperty]
    ObservableCollection<ToDoLists> todoItems;

    [ObservableProperty]
    string todo;
   

    [ObservableProperty]
    ObservableCollection<ToDoLists> complateda;
    public MainViewModel()
    {
        _toDoListService = new ToDoListService();
        TodoItems = new ObservableCollection<ToDoLists>(); 
        Complateda = new ObservableCollection<ToDoLists>();
    }


    public async Task InitializeAsync()
    {
        int userId = Preferences.Get("UserId", -1);
        if (userId == -1) return;

        var toDoLists = await _toDoListService.GetToDoLists(userId);
        if (toDoLists != null)
        {
            foreach (var item in toDoLists)
            {
                if (item.IsComplete == false)
                {
                    TodoItems.Add(item);
                }
                else
                {
                    Complateda.Add(item);
                }
            }
        }
    }


    [RelayCommand]
    private async Task AddToDo()
    {
        if (string.IsNullOrWhiteSpace(Todo))
            return;
        int id = Preferences.Get("UserId", -1);
        var newToDoList = new ToDoLists
        {
            Title = Todo,
            UserId = id,
            Description = "Description is null",
            IsComplete = false,
            IsActive = true,
        };

        var createdToDoList = await _toDoListService.ToDoLists(newToDoList);

        if (createdToDoList != null)
        {
            TodoItems.Add(createdToDoList);
            Todo = string.Empty;
        }
    }
    

    [RelayCommand]
    async Task Delete(object item)
    {
        if (item is ToDoLists toDoLists)
        {
            var succes = await _toDoListService.SetInactive(toDoLists);
            if (succes != null)
            {
                TodoItems.Remove(toDoLists);
                Complateda.Remove(toDoLists);
            }
        }
    }




    [RelayCommand]
    async Task Complated(ToDoLists completedTodo)
    {
        if (completedTodo is ToDoLists toDoLists)
        {
            var updatedToDo = await _toDoListService.ToDoComplated(toDoLists);
            if (updatedToDo != null)
            {
                Complateda.Add(completedTodo);
                TodoItems.Remove(completedTodo);
            }
        }
    }


    [RelayCommand]
    private async Task Logout()
    {
        // Oturum bilgilerini temizle
        Preferences.Remove("UserId");
        Preferences.Remove("UserName");
        Preferences.Remove("UserEmail");
        Preferences.Remove("UserPassword");

        // Oturum kapandığında giriş sayfasına yönlendir
        await Application.Current.MainPage.Navigation.PopToRootAsync();
    }

}