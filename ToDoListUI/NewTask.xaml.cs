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
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : Window
    {
        TasksWindow tw;
        public NewTask(TasksWindow tw)
        {
            this.tw = tw;
            InitializeComponent();
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            task.Name = nameTextBox.Text;

            DateTime? selectedDate = endDatePicker.SelectedDate;
            if (selectedDate != null )
            {
                task.EndDate = selectedDate.Value;
            }
            
            task.Description = descTextBox.Text;
            task.Priority = (int)prioritySlider.Value;
            task.InsertDate = DateTime.Now;

            task = GlobalConfig.TaskDao.CreateTask(task, (int)GlobalConfig.LoggedUser.Id).Result;
            GlobalConfig.LoggedUser.Tasks.Add(task);

            tw.RefrestListbox();
            this.Close();
        }
    }
}
