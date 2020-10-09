using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloApi.Services
{
    public class HelloService : IHelloService
    {
        private string _id;

        public HelloService()
        {
            _id = Guid.NewGuid().ToString();
        }
        public void Hello()
        {
            Console.WriteLine($"hello dotnet core:{_id}");
        }
    }
}
