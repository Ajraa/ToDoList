using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
