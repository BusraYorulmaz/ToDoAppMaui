using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppToDo.Models;
using MauiAppToDo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAppToDo.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly ToDoListService _toDoListService;

    [ObservableProperty]
    string to_do;  
     

    [ObservableProperty]
    ObservableCollection<String> todoItems;

    [ObservableProperty]
    ObservableCollection<String> complatedList;

    
    public MainViewModel()
    {
        _toDoListService= new ToDoListService();
        TodoItems = new ObservableCollection<String>();
        ComplatedList = new ObservableCollection<String>();
    }

    private List<ToDoLists> _toDoLists;

    public List<ToDoLists> ToDoLists
    {
        get => _toDoLists;
        set => SetProperty(ref _toDoLists, value);
    }

     
    public async Task InitializeAsync()
    {
        int userId = Preferences.Get("UserId", -1);
        if (userId == -1) return;

        var toDoLists = await _toDoListService.GetToDoLists(userId);
        if (toDoLists != null)
        {
            foreach(var item in toDoLists)
            {
                TodoItems.Add(item.Title);
            }
        }
    }


    [RelayCommand]
    private async Task AddToDo()
    {
        if (string.IsNullOrWhiteSpace(To_do))
            return;
        int id = Preferences.Get("UserId", -1);
        var newToDoList = new ToDoLists
        {
            Title = To_do,          
            UserId =id,
            Description = "null",
            IsComplete=0,       
        };

        var createdToDoList = await _toDoListService.ToDoLists(newToDoList.UserId, newToDoList);

        if (createdToDoList != null)
        {
             TodoItems.Add(createdToDoList.Title);
             To_do = string.Empty;          
        }     
    }


    [RelayCommand]
    void Delete(string del)
    {
        if (TodoItems.Contains(del))
        {
            TodoItems.Remove(del);
        }
    }

    [RelayCommand]
    void Complated(string completedTodo)
    {

        ComplatedList.Add(completedTodo);
        TodoItems.Remove(completedTodo);

    }




}