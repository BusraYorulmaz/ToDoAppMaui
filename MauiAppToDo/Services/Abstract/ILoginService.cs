using MauiAppToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppToDo.Services.Abstract
{
    public interface ILoginService
    {
        Task<Users> Login(string username, string password);
    }
}
