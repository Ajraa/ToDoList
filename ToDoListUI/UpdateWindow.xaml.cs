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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        Task task;
        TasksWindow tw;
        public UpdateWindow(Task task, TasksWindow tw)
        {
            InitializeComponent();
            this.task = task;
            nameTextBox.Text = task.Name;
            descTextBox.Text = task.Description;
            endDatePicker.SelectedDate = task.EndDate;
            prioritySlider.Value = task.Priority;
            this.tw = tw;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            task.Name = nameTextBox.Text;
            task.Description = descTextBox.Text;
            task.EndDate = (DateTime)endDatePicker.SelectedDate;
            task.Priority = (int)prioritySlider.Value;
            GlobalConfig.TaskDao.UpdateTask(task);
            tw.RefrestListbox();
            
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalConfig.TaskDao.DeleteTask((int)task.Id);
            
            GlobalConfig.LoggedUser.Tasks.Remove(task);
            tw.RefrestListbox();
            this.Close();
        }
    }
}
