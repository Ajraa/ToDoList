using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Data.DataAccessObjects;
using DataLibrary;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskDao _taskDao;
        public TasksController(ITaskDao taskDao)
        {
            _taskDao = taskDao;
        }

        [HttpPost("create/{id}")]
        public IActionResult CreateTask([FromBody]Task task, int id)
        {
            task = _taskDao.CreateTask(task, id);

            if (task.Id != null)
                return Ok(task);
            else 
                return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTask(int id)
        {
            return Ok(_taskDao.DeleteTask(id));
        }

        [HttpGet("taskid/{id}")]
        public IActionResult GetTask(int id)
        {
            Task task = _taskDao.GetTaskById(id);
            if (task.Id != null)
                return Ok(task);
            else
                return BadRequest();
        }

        [HttpGet("userid/{id}")]
        public IActionResult GetByUserId(int id)
        {
            List<Task> tasks = _taskDao.GetTasksByUserId(id);
            return Ok(tasks);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody]Task task) 
        {
            return Ok(_taskDao.UpdateTask(task));
        }
    }
}