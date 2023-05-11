using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppToDo.Models;
using MauiAppToDo.Services;
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
    string to_do;

    [ObservableProperty]
    ObservableCollection<ToDoLists> todoItems;

    [ObservableProperty]
    ObservableCollection<ToDoLists> complatedList;

    public MainViewModel()
    {
        _toDoListService = new ToDoListService();
        TodoItems = new ObservableCollection<ToDoLists>();
        ComplatedList = new ObservableCollection<ToDoLists>();
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
                TodoItems.Add(item);
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
            UserId = id,
            Description = "Description is null",
            IsComplete = false,
            IsActive = true,
        };

        var createdToDoList = await _toDoListService.ToDoLists(newToDoList.UserId, newToDoList);

        if (createdToDoList != null)
        {
            TodoItems.Add(createdToDoList);
            To_do = string.Empty;
        }
    }


    [RelayCommand]
    async Task Delete(object item)
    {
        if (item is ToDoLists toDoLists)
        {
            var succes = _toDoListService.SetInactive(toDoLists);
            if (succes != null)
            {
                TodoItems.Remove(toDoLists);
            }
            TodoItems.Remove(toDoLists);
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
                ComplatedList.Add(completedTodo);
                TodoItems.Remove(completedTodo);
            }
        }
    }




}