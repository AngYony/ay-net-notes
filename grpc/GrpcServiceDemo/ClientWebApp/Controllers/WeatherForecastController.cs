using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer.Web.Protos;
using GrpcServiceDemo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClientWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeiHuDai.WeiHuDaiClient weiHuDaiClient;
        private readonly IHostEnvironment hostEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            WeiHuDai.WeiHuDaiClient weiHuDaiClient,
            IHostEnvironment hostEnvironment

        )
        {
            _logger = logger;
            this.weiHuDaiClient = weiHuDaiClient;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IEnumerable<WeiHuDaiDto> Get()
        {
          

            return this.weiHuDaiClient.GetWeiHuDaiList(new WeiHuDaiListFrom { }).WhdDto;
        }

        [HttpGet("employee")]
        public Employee GetEmployee()
        {
            //传入元数据
            var md = new Metadata
            {
                {"username","jack" },
                {"role","admin" }
            };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new EmployeeService.EmployeeServiceClient(channel);

            return client.GetByNo(new GetByNoRequest
            {
                No = 1994
            }, md).Employee;
        }

        [HttpGet("all")]
        public async Task<List<Employee>> GetAll()
        {
            //传入元数据

            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new EmployeeService.EmployeeServiceClient(channel);

            List<Employee> employees = new List<Employee>();

            using var call = client.GetAll(new GetAllRequest());
            var responseStream = call.ResponseStream;
            while (await responseStream.MoveNext())
            {
                employees.Add(responseStream.Current.Employee);
            }

            return employees;
        }

        [HttpGet("addPhoto")]
        public async Task AddPhoto()
        {
            //传入元数据
            var md = new Metadata
            {
                {"username","jack" },
                {"role","admin" }
            };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new EmployeeService.EmployeeServiceClient(channel);

            var s = Path.Combine(hostEnvironment.ContentRootPath, "doc", "abc.png");

            FileStream fs = System.IO.File.OpenRead(s);
            using var call = client.AddPhoto(md);
            var stream = call.RequestStream;

            while (true)
            {
                byte[] buffer = new byte[1024];
                int numRead = await fs.ReadAsync(buffer, 0, buffer.Length);
                if (numRead == 0)
                {
                    break;
                }
                if (numRead < buffer.Length)
                {
                    Array.Resize(ref buffer, numRead);
                }

                await stream.WriteAsync(new AddPhotoRequest
                {
                    Data = ByteString.CopyFrom(buffer)
                });
            }

            await stream.CompleteAsync();
            var res = await call.ResponseAsync;
            Console.WriteLine(res.IsOk);
        }

        [HttpPost("saveAll")]
        public async Task SaveAll()
        {
            var employees = new List<Employee>{
                new Employee{
                    No=222,
                    FirstName="张三",
                    Id=5,
                    Salary=143.23f,
                     Status= EmployeeStatus.OnVacation,
                      LastModified=Timestamp.FromDateTime(DateTime.UtcNow),
                },
                new Employee{
                    No=444,
                    FirstName="李四",
                    Id=6,
                    Salary=33.33f
                }
            };

            //传入元数据
            var md = new Metadata
            {
                {"username","jack" },
                {"role","admin" }
            };
            using var channel = GrpcChannel.ForAddress("http://localhost:6001");
            var client = new EmployeeService.EmployeeServiceClient(channel);

            using var call = client.SaveAll();
            var requestStream = call.RequestStream;
            var responseStream = call.ResponseStream;

            //var responseTask = Task.Run(async () =>
            //{
            //    while (await responseStream.MoveNext())
            //    {
            //        Console.WriteLine($"接收：{responseStream.Current.Employee}");
            //    }
            //});

            var responseTask = await Task.Factory.StartNew(async () =>
            {
                while (await responseStream.MoveNext())
                {
                    Console.WriteLine($"接收：{responseStream.Current.Employee}");
                }
            });

            foreach (var employee in employees)
            {
                await requestStream.WriteAsync(new EmployeeRequest
                {
                    Employee = employee
                });
            }
            await requestStream.CompleteAsync(); //通知服务端请求结束
            //await responseTask必须位于requestStream.CompleteAsync()之后
            await responseTask;
        }
    }
}