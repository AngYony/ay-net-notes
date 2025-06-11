using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Models
{
    internal class TodoInfo
    {
        public int Id { get; set; }
        public string   Title{ get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public string Color { get; set; }
    }
}
