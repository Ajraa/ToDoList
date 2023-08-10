using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoListUI.APIAccess;
using Task = DataLibrary.Task;

namespace ToDoListUI
{
    /// <summary>
    /// Interaction logic for TasksWindow.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {
        public TasksWindow()
        {
            InitializeComponent();
            userLabel.Content = GlobalConfig.LoggedUser.Username;
            GlobalConfig.LoggedUser.Tasks = GlobalConfig.TaskDao.GetTasksByUserId((int)GlobalConfig.LoggedUser.Id).Result;
            GlobalConfig.LoggedUser.Tasks = GlobalConfig.LoggedUser.Tasks.OrderByDescending(t => t.Priority).ToList();
            //taskListBox.ItemsSource = GlobalConfig.LoggedUser.Tasks;

            GridView gw = new GridView();
            taskListBox.View = gw;
            gw.Columns.Add(new GridViewColumn {
            Header = "Name", DisplayMemberBinding = new Binding("Name")});

             gw.Columns.Add(new GridViewColumn {
            Header = "Priority", DisplayMemberBinding = new Binding("Priority")});

            gw.Columns.Add(new GridViewColumn {
            Header = "Insert Date", DisplayMemberBinding = new Binding("InsertDate")});

            gw.Columns.Add(new GridViewColumn {
            Header = "End Date", DisplayMemberBinding = new Binding("EndDate")});

            GlobalConfig.LoggedUser.Tasks.ForEach(t => taskListBox.Items.Add(t));
            
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalConfig.LoggedUser = null;
            new MainWindow().Show();
            this.Close();
        }

        private void newTaskButton_Click(object sender, RoutedEventArgs e)
        {
            new NewTask(this).Show();
            
        }

        public void RefrestListbox()
        {
            GlobalConfig.LoggedUser.Tasks = GlobalConfig.LoggedUser.Tasks.OrderByDescending(t => t.Priority).ToList();
            taskListBox.Items.Refresh();
        }

        private void taskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task task = taskListBox.SelectedItem as Task;
            new UpdateWindow(task, this).Show();
        }
    }
}
