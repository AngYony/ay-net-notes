using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionSample.Models
{
    internal class Student:BaseStudent, IStudent
    {
        public void SayName(){
            Console.WriteLine("我的名字叫："+StuName);
        }

        public void Sport(string sportName)
        {
            Console.WriteLine($"我最喜欢的运动是：{sportName}");
        }
    }
}
