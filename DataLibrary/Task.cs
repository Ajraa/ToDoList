using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class Task
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }

        public override string ToString()
        {
            return InsertDate + ", " + Name;
        }
    }
}
