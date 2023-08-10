using System.Collections.Generic;
using DataLibrary;

namespace ToDoList.Data.DataAccessObjects
{
    public interface ITaskDao
    {
        List<Task> GetTasksByUserId(int Id);
        Task GetTaskById(int Id);
        Task CreateTask(Task task, int id);
        bool DeleteTask(int Id);
        bool UpdateTask(Task task);
    }
}
