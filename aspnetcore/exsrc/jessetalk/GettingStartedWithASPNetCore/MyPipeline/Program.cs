using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPipeline
{
    class Program
    {

        public static List<Func<RequestDelegate, RequestDelegate>> _list 
        = new List<Func<RequestDelegate, RequestDelegate>>();


        static void Main(string[] args)
        {

            //Use方法负责添加到集合中
            Use(next => {
                return context => {
                    Console.WriteLine("1");

                    return next.Invoke(context);

                };

            });

            Use(next => {
                return context => {
                    Console.WriteLine("2");
                    return next.Invoke(context);
                };

            });
            
                                          
            RequestDelegate next_end =  (Context) => { 
                Console.WriteLine("end...");
                return Task.CompletedTask;
            };

            
            _list.Reverse();
            foreach(var middleware in _list)
            {
                //将集合中的各个middleware串联起来
                //middleware对应上述Use()方法的传入的方法体内容
                //next_end对应Use()方法体中的next实参值，
                //遍历第一遍时：会在第一个元素后面调用next_end，返回包含第一个元素和next_end的方法
                //遍历第二遍时：将上一遍返回的再次追加调用在第二个元素的后面，
                //fun1+next_end
                //fun2+fun1+next_end
                //fun3+fun2+fun1+next_end
                //由此需要将list集合反转
                next_end = middleware.Invoke(next_end);
            }
            //当遍历完所有后，next_end是包含所有中间件的任务：fun3+fun2+fun1+next_end
            //此处的next_end指向：
            //return context => {
            //    Console.WriteLine("1");

            //    return next.Invoke(context);

            //};
            next_end.Invoke(new Context());
            Console.ReadLine();
                                            
        }
        

        public static void Use(Func<RequestDelegate,RequestDelegate> middleware){
            _list.Add(middleware);
        }
    }
}
