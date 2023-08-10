using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Controls;
using System.Windows;
using ToDoList.Data.DataAccessObjects;
using Newtonsoft.Json;

namespace ToDoListUI.APIAccess.DAOs
{
    internal class UserDao
    {
        private HttpClient client;
        public UserDao(HttpClient client) 
        { 
            this.client = client;
        }

        private User StringToUser(string userString)
        {
            User user = JsonConvert.DeserializeObject<User>(userString);

            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            
            var query = HttpUtility.ParseQueryString(String.Empty);
            query["username"] = username;
            query["password"] = password;
            string queryString = query.ToString();

            var resp = await client.GetAsync(URIs.UserLoginURI + queryString).ConfigureAwait(false);

            string userString = resp.Content.ReadAsStringAsync().Result;

            if (userString != "Uživatel neexistuje")
            {
                return StringToUser(userString);

            }
            else
            {
                return null;
            }
        }

        public async Task<User> Register(string username, string password)
        {
            HttpContent content = new StringContent(URIs.UserParameters(username, password)
                , Encoding.UTF8, "application/json");
            HttpResponseMessage resp = await client.PostAsync(URIs.UserRegisterURI + URIs.UserParameters(username, password), content).ConfigureAwait(false);
            string userString = resp.Content.ReadAsStringAsync().Result;
            return StringToUser(userString);
        }
    }
}
