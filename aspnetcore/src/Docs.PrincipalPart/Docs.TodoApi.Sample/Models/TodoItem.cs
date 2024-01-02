using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.TodoApi.Sample.Models
{
    public class TodoItem
    {
        public long Id{ get; set; }
        public string Name{ get; set; }
        public bool IsComplete{ get; set; }
    }
}
