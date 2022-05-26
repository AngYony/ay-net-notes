using GrpcServer.Web.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServiceDemo.Services
{
    public class DbData
    {
        public static List<Employee> Employees = new List<Employee>{
            new Employee{
                Id =1,
                 No = 1994,
                  FirstName="孙悟空",
                   Salary=1111
            },
            new Employee{
                Id =2,
                 No = 1995,
                  FirstName="猪八戒",
                   Salary=34342
            },
            new Employee{
                Id =1,
                 No = 1996,
                  FirstName="唐僧",
                   Salary=1222
                }
        };
    }


}
