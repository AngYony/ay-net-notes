using Grpc.Core;
using GrpcServer.Web.Protos;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceDemo.Services
{
    public class MyEmployeeService : EmployeeService.EmployeeServiceBase
    {
        private readonly ILogger<MyEmployeeService> logger;

        public MyEmployeeService(ILogger<MyEmployeeService> logger)
        {
            this.logger = logger;
        }

        public override Task<EmployeeResponse> GetByNo(GetByNoRequest request, ServerCallContext context)
        {
            //获取传入的元数据
            var md = context.RequestHeaders;
            foreach (var pair in md)
            {
                this.logger.LogInformation($"{pair.Key}:{pair.Value}");
            }
            var employee = DbData.Employees.FirstOrDefault(x => x.No == request.No);
            if (employee != null)
            {
                var response = new EmployeeResponse
                {
                    Employee = employee
                };
                return Task.FromResult(response);
            }
            throw new Exception($"employee not found with no:{request.No} ");
        }


        public override async Task GetAll(GetAllRequest request, IServerStreamWriter<EmployeeResponse> responseStream, ServerCallContext context)
        {
            foreach (var employee in DbData.Employees)
            {
                await responseStream.WriteAsync(new EmployeeResponse
                {
                    Employee = employee
                });
            }
        }

        public override async Task<AddPhotoResponse> AddPhoto(IAsyncStreamReader<AddPhotoRequest> requestStream, ServerCallContext context)
        {
            //获取传入的元数据
            var md = context.RequestHeaders;
            foreach (var pair in md)
            {
                this.logger.LogInformation($"{pair.Key}:{pair.Value}");
            }

            var data = new List<byte>();
            while (await requestStream.MoveNext())
            {
                Console.WriteLine("接收：" + requestStream.Current.Data.Length);
                data.AddRange(requestStream.Current.Data);
            }

            Console.WriteLine($"接收到的字节长度：{data.Count}，（{data.Count / 1024}KB）");
            return new AddPhotoResponse { IsOk = true };

        }

        public override async Task SaveAll(IAsyncStreamReader<EmployeeRequest> requestStream, IServerStreamWriter<EmployeeResponse> responseStream, ServerCallContext context)
        {
            //先处理客户端
            while(await requestStream.MoveNext()){
                var employee= requestStream.Current.Employee;
                lock (this)
                {
                    DbData.Employees.Add(employee);
                }
                await responseStream.WriteAsync(new EmployeeResponse
                {
                    Employee = employee
                });
            }

            Console.WriteLine("Employees:");
            foreach(var emp in DbData.Employees){
                Console.WriteLine(emp);
            }
        }
    }
}
