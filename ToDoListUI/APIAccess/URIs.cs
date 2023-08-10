using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListUI.APIAccess
{
    public static class URIs
    {
        private static string MainURI = "https://localhost:5001/";
        private static string UsersURI = MainURI + "api/Users/";
        private static string TasksURI = MainURI + "api/Tasks/";
        public static string UserRegisterURI = UsersURI + "register";
        public static string UserLoginURI = UsersURI + "login?";
        public static string TasksGetByUserId = TasksURI + "userid/";
        private static string TasksCreate = TasksURI + "create/";
        public static string TasksUpdate = TasksURI + "update";
        private static string TasksDelete = TasksURI + "delete/";

        public static string UserParameters(string username, string password)
        {
            return string.Format("?username={0}&password={1}", username, password);
        }

        public static string CreateUserURI() 
        {
            return TasksCreate + GlobalConfig.LoggedUser.Id;
        }

        public static string DeleteTaskUri(int id) 
        {
            return TasksDelete + id;
        }
    }
}
