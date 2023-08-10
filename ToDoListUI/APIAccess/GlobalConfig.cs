using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoListUI.APIAccess.DAOs;

namespace ToDoListUI.APIAccess
{
    internal class GlobalConfig
    {
        private static HttpClient httpClient = new HttpClient();
        public static User LoggedUser { get; set; }
        public static UserDao UserDao = new UserDao(httpClient);
        public static TaskDao TaskDao = new TaskDao(httpClient);
    }
}
