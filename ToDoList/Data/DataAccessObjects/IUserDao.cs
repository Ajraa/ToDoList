using DataLibrary;

namespace ToDoList.Data.DataAccessObjects
{
    public interface IUserDao
    {
        User Login(string username, string password);
        User Register(User user, string password);
    }
}
