using System;
using System.Collections.Generic;

namespace Collections_Sample
{
    public struct EmployeeId : IEquatable<EmployeeId>
    {
        private readonly char prefix;
        private readonly int number;

        public EmployeeId(string id)
        {
            //System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(id != null);
            prefix = (id.ToUpper())[0];
            int numLength = id.Length - 1;
            try
            {
                number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
            }
            catch (Exception)
            {
                throw new Exception("EmployeeId格式错误");
            }
        }

        public override string ToString()
        {
            return prefix.ToString() + $"{number,6:000000}";
        }

        //重写GetHashCode()方法
        public override int GetHashCode()
        {
            //此条语句只是为了使得到的值能够尽可能的平均到int范围
            //将数字向左移动16位，再与原数字进行异或操作，得到的结果乘以16进制数15051505
            return (number ^ number << 16) * 0x15051505;
        }

        //必须实现Equals()方法
        public bool Equals(EmployeeId other)
        {
            //return (_prefix == other?._prefix && _number == other?._number);
            return (prefix == other.prefix && number == other.number);
        }

        public override bool Equals(object obj)
        {
            return Equals((EmployeeId)obj);
        }

        //使用 operator 关键字重载内置运算符==
        public static bool operator ==(EmployeeId left, EmployeeId right)
        {
            return left.Equals(right);
        }

        //使用 operator 关键字重载内置运算符!=
        public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
    }

    public class Employee
    {
        private string name;
        private decimal salary;
        private readonly EmployeeId id;

        public Employee(EmployeeId id, string name, decimal salary)
        {
            this.id = id;
            this.name = name;
            this.salary = salary;
        }

        public override string ToString()
        {
            return $"{id.ToString()}:{name,-20} {salary:C}";
        }
    }

    internal class DictionaryDemo
    {
        public static void Run()
        {
            var idTony = new EmployeeId("C3755");
            var tony = new Employee(idTony, "Tony Stewart", 379025.00m);

            var idCarl = new EmployeeId("F3547");
            var carl = new Employee(idCarl, "Carl Edwards", 403466.00m);

            var idKevin = new EmployeeId("C3386");
            var kevin = new Employee(idKevin, "kevin Harwick", 415261.00m);

            //字典使用EmployeeId对象来索引
            var employees = new Dictionary<EmployeeId, Employee>(5)
            {
                [idTony] = tony,
                [idCarl] = carl,
                [idKevin] = kevin
            };

            foreach (var employee in employees.Values)
            {
                Console.WriteLine(employee);
            }

            
        }
    }
}