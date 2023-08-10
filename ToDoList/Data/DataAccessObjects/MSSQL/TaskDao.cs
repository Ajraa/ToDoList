using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataLibrary;

namespace ToDoList.Data.DataAccessObjects.MSSQL
{
    public class TaskDao : ITaskDao
    {
        private readonly IConfiguration _configuration;

        public TaskDao(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public Task CreateTask(Task task, int id)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "INSERT INTO tasks(name, insert_date, end_date, description, priority, userID) " +
                    "OUTPUT INSERTED.Id " +
                   "VALUES (@name, @insertDate, @endDate, @description, @priority, @Id)";
                var parameters = new DynamicParameters();
                parameters.Add("@name", task.Name);
                parameters.Add("@insertDate", task.InsertDate);
                parameters.Add("@endDate", task.EndDate);
                parameters.Add("@description", task.Description);
                parameters.Add("@priority", task.Priority);
                parameters.Add("@id", id);
                task.Id = conn.ExecuteScalar<int>(sql, parameters);
            }
            return task;
        }

        public bool DeleteTask(int Id)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "DELETE FROM tasks WHERE id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                conn.Execute(sql, parameters);
               return true;
            }
            return false;
        }

        public Task GetTaskById(int Id)
        {
            Task task = null;
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "SELECT t.id, t.name, t.insert_date InsertDate, t.end_date EndDate, t.description, t.priority " +
                    "FROM tasks t " +
                    "WHERE t.id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                task = conn.QuerySingle<Task>(sql, parameters);
                    }
            return task;
        }

        public List<Task> GetTasksByUserId(int Id)
        {
            List<Task> tasks = new List<Task>();
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "SELECT t.id, t.name, t.insert_date InsertDate, t.end_date EndDate, t.description, t.priority " +
                    "FROM tasks t " +
                    "JOIN users u ON u.id = t.userID " +
                    "WHERE u.id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                tasks = conn.Query<Task>(sql, parameters).ToList();
            }
            return tasks;
        }

        public bool UpdateTask(Task task)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "UPDATE tasks " +
                    "SET name = @name, end_date = @date, description = @description, priority = @priority " +
                    "WHERE id = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@name", task.Name);
                parameters.Add("@date", task.EndDate);
                parameters.Add("@description", task.Description);
                parameters.Add("@priority", task.Priority);
                parameters.Add("@id", task.Id);
                conn.Execute(sql, parameters);
                return true;
            }
            return false;
        }
    }
}
