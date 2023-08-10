using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data.DataAccessObjects;
using DataLibrary;
using System;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDao _userDao;
        public UsersController(IUserDao userDao) 
        {
            this._userDao = userDao;
        }

        [HttpGet("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            User user = _userDao.Login(username, password);


            if (user != null)
                return Ok(user);
            else
                return Ok("Uživatel neexistuje");

        }

        [HttpPost("register")]
        public IActionResult Register([FromQuery]string username, [FromQuery]string password)
        {
            Console.WriteLine(username);
            User user = new User();
            user.Username = username;
            user = _userDao.Register(user, password);

            if (user.Id != null)
                return Ok(user);
            else
                return BadRequest();
            
        }
    }
}
