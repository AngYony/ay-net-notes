using System;
using System.Collections.Generic;

namespace Delegates_Sample
{
    internal class BubbleSorter
    {
        public static void Sort(int[] sortArray)
        {
            bool swapped = true;
            do
            {
                swapped = false;
                for (int i = 0; i < sortArray.Length - 1; i++)
                {
                    if (sortArray[i] > sortArray[i + 1])
                    {
                        int temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }

        public static void Sort<T>(IList<T> sortArray, Func<T, T, bool> comparison)
        {
            bool swapped = true;
            do
            {
                swapped = false;
                for (int i = 0; i < sortArray.Count - 1; i++)
                {
                    if (comparison(sortArray[i + 1], sortArray[i]))
                    {
                        T temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
    }

    internal class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; private set; }

        public override string ToString() => $"{Name},{Salary:C}";

        public Employee(string name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        public static bool CompareSalary(Employee e1, Employee e2) => e1.Salary < e2.Salary;
    }

    public static class BubbleSorter_Program
    {
        public static void Run()
        {
            #region 普通排序调用

            //var nums = new[] { 41, 24, 13, 14 };
            //BubbleSorter.Sort(nums);
            //foreach (var n in nums)
            //{
            //    Console.WriteLine(n);
            //}

            #endregion 普通排序调用

            Employee[] employees = {
                new Employee("小明",8000),
                new Employee("小芳",9800),
                new Employee("小黑",4000),
                new Employee("小米",13000),
                new Employee("小马",12000)
            };

            //BubbleSorter.Sort<Employee>(employees, Employee.CompareSalary);
            BubbleSorter.Sort(employees, Employee.CompareSalary);

            ForeachWrite(employees);
        }

        public static void ForeachWrite<T>(T[] list)
        {
            
            foreach (T item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}