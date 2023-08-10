using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToDoListUI.APIAccess;
using Newtonsoft.Json;

namespace ToDoListUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalConfig.LoggedUser = GlobalConfig.UserDao.Login(usernameTextBox.Text, passwordBox.Password).Result;
            new TasksWindow().Show();
            this.Close();
        }

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            
            GlobalConfig.LoggedUser = GlobalConfig.UserDao.Register(usernameTextBox.Text, passwordBox.Password).Result;

            new TasksWindow().Show();
            this.Close();
        }
    }
}
